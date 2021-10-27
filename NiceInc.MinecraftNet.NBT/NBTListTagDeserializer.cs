using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTListTagDeserializer : ITagDeserializer
    {
        public ITag Deserialize(Stream stream, NBTDeserializer deserializer)
        {
            TagType type = (TagType)stream.ReadNonEndByte();

            int length = ((IValueTag<int>)deserializer.DeserializeSingleHeadless(stream, TagType.Int)).Value;

            var items = new ITag[length];

            for (int i = 0; i < length; i++) items[i] = deserializer.DeserializeSingleHeadless(stream, type);

            return new NBTListTag(type, items);
        }
    }
}
