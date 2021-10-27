using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTDeserializer
    {
        private Dictionary<TagType, ITagDeserializer> deserializers = new Dictionary<TagType, ITagDeserializer>();

        public bool AddDeserializer(TagType type, ITagDeserializer deserializer)
        {
            return deserializers.TryAdd(type, deserializer);
        }
        public ITagDeserializer GetDeserializer(TagType type)
        {
            return deserializers.TryGetValue(type, out var val) ? val : null;
        }
        public bool TryGetDeserializer(TagType type, out ITagDeserializer deserializer)
        {
            return deserializers.TryGetValue(type, out deserializer);
        }

        public IEnumerable<ITagDeserializer> Deserializers => deserializers.Values;

        public HeadedNBTTag DeserializeSingle(Stream stream)
        {
            int type = stream.ReadByte();
            if (type == -1) throw new NBTEndOfDataException();
            if (type == 0) return null;

            var rawLength = stream.ReadNonEndBytes(2);
            var length = BinaryPrimitives.ReadUInt16BigEndian(rawLength);
            var data = stream.ReadNonEndBytes(length);
            var name = new string(data.Select(v => (char)v).ToArray());

            return new HeadedNBTTag(new NBTTagHead(name, (TagType)type), DeserializeSingleHeadless(stream, (TagType)type));
        }
        // single... just like me
        public ITag DeserializeSingleHeadless(Stream stream, TagType type)
        {
            if (!TryGetDeserializer(type, out var deserializer)) throw new NBTUnknownTagTypeException((byte)type);

            return deserializer.Deserialize(stream, this);
        }

        public ITag Deserialize(Stream stream)
        {
            return DeserializeSingle(stream).Tag;
        }
        public ITag DeserializeFromStream(Stream reader, bool compressed = true)
        {
            if (compressed) {
                using (var str = new GZipStream(reader, CompressionMode.Decompress)) {
                    var list = new List<byte>();

                    int i;
                    while ((i = str.ReadByte()) != -1) {
                        list.Add((byte)i);
                    }

                    using (var memStr = new MemoryStream(list.ToArray()))
                        return Deserialize(memStr);
                }
            }
            return Deserialize(reader);
        }
        public ITag DeserializeFromStream(Stream reader)
        {
            using (var str = new GZipStream(reader, CompressionMode.Decompress)) {
                var list = new List<byte>();

                int i;
                while ((i = str.ReadByte()) != -1) {
                    list.Add((byte)i);
                }

                using (var memStr = new MemoryStream(list.ToArray()))
                    return Deserialize(memStr);
            }
        }

        public static NBTDeserializer Default()
        {
            var d = new NBTDeserializer();
            d.AddDeserializer(TagType.Byte, new NBTValueTagDeserializer<sbyte>(TagType.Byte));
            d.AddDeserializer(TagType.Short, new NBTValueTagDeserializer<short>(TagType.Short));
            d.AddDeserializer(TagType.Int, new NBTValueTagDeserializer<int>(TagType.Int));
            d.AddDeserializer(TagType.Long, new NBTValueTagDeserializer<long>(TagType.Long));
            d.AddDeserializer(TagType.Float, new NBTValueTagDeserializer<float>(TagType.Float));
            d.AddDeserializer(TagType.Double, new NBTValueTagDeserializer<double>(TagType.Double));
            d.AddDeserializer(TagType.String, new NBTStringTagDeserializer());
            d.AddDeserializer(TagType.Compound, new NBTCompoundTagDeserializer());
            d.AddDeserializer(TagType.List, new NBTListTagDeserializer());
            d.AddDeserializer(TagType.IntArray, new NBTArrayTagDeserializer<int>(TagType.IntArray));
            d.AddDeserializer(TagType.LongArray, new NBTArrayTagDeserializer<long>(TagType.LongArray));
            d.AddDeserializer(TagType.ByteArray, new NBTArrayTagDeserializer<byte>(TagType.ByteArray));

            return d;
        }
    }
}
