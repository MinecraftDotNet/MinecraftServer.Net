using NiceInc.MinecraftNet.NBT;
using System.Collections.Generic;
using System.IO;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Zip.Compression;
using System;

namespace NiceInc.MinecraftNet.Save
{
    public class BitStreamReader
    {
        public Stream BaseStream { get; set; }
        private int bitPos = 0;
        private byte currByte = 0;

        public long Position => BaseStream.Position * 8 - 8 + bitPos;

        public bool Ended { get; private set; }
        public void Seek(long offset, SeekOrigin origin) {
            var i = offset;

            switch (origin) {
                case SeekOrigin.Current:
                    i += Position;
                    break;
                case SeekOrigin.End:
                    i += BaseStream.Length << 3;
                    break;
            }

            BaseStream.Seek(i, SeekOrigin.Begin);
            bitPos = unchecked((int)i) & 0b111;

            int c = BaseStream.ReadByte();
            if (c == -1) Ended = true;
            else {
                currByte = (byte)c;
            }
        }
        public ulong Read(int count)
        {
            if (count == 0) return 0;
            if (count > 64 || count < 0)
                throw new ArgumentOutOfRangeException("count", count, "Count must be between 0 and 64");

            ulong res = 0;
            int offset = 0;

            for (int i = 0; i < count >> 3; i++) {

            }
        }
    }
    public class Block
    {
        public int ID { get; set; }
        public NBTCompoundTag NBT { get; }

        public Block(int id, NBTCompoundTag nbt = null)
        {
            ID = id;
            NBT = nbt ?? new NBTCompoundTag();
        }
    }
    public class Chunk
    {
        public class BiomeData
        {
            private NBTArrayTag<int> list;
            public int this[int x, int y, int z] {
                get => list.Elements[x >> 2 + z << 2 + y << 6];
                set => list.Elements[x >> 2 + z << 2 + y << 6] = value;
            }

            internal BiomeData(NBTArrayTag<int> list)
            {
                this.list = list;
            }
        }
        public class Section
        {

        }

        private NBTCompoundTag data;
        public BiomeData Biomes;

        public bool Full => data.GetValue<string>("Status") == "full";
        public int X {
            get => data.GetValue<int>("xPos");
            set => data.Set("xPos", value);
        }
        public int Y {
            get => data.GetValue<int>("yPos");
            set => data.Set("yPos", value);
        }

        public Chunk(NBTCompoundTag data)
        {
            this.data = data;
            Biomes = new BiomeData(data.TryGetArray<int>("Biomes", out var _bd) ?
                _bd :
                new NBTArrayTag<int>(new int[1024], TagType.IntArray)
            );
        }

        public static void PrintIdentation(int i, TextWriter w)
        {
            for (; i > 0; i--) w.Write(" ");
        }
        public static void Print(ITag tag, TextWriter w, int identation = 0, bool newLine = true)
        {
            bool first = true;
            if (tag is NBTCompoundTag compound) {
                int i = 0;
                foreach (var _tag in compound) {
                    if (newLine || !first) {
                        w.WriteLine();
                        PrintIdentation(identation, w);
                    }
                    w.Write(_tag.Key + ": ");
                    Print(_tag.Value, w, identation + 4);
                    first = false;

                    if (++i != compound.Count) w.Write(",");
                }
            }
            else if (tag is NBTListTag list) {
                if (list.Count == 0) w.Write("[ ]");
                int i = 0;
                foreach (var _tag in list) {
                    if (newLine || !first) {
                        w.WriteLine();
                        PrintIdentation(identation, w);
                    }
                    w.Write("- ");
                    Print(_tag, w, identation + 2, false);
                    first = false;
                    if (++i != list.Count) w.Write(",");
                }
            }
            else if (tag is NBTArrayTag<int> array) {
                w.Write($"[ { string.Join(", ", array.Elements) } ]");
            }
            else if (tag is NBTArrayTag<long> larray) {
                w.Write($"[ { string.Join(", ", larray.Elements) } ]");
            }
            else if (tag is NBTArrayTag<byte> barray) {
                w.Write($"[ { string.Join(", ", barray.Elements) } ]");
            }
            else w.Write(tag.ToString());
        }

        private static NBTDeserializer deserializer = NBTDeserializer.Default();
        public static Chunk Deserialize(Stream stream)
        {
            var length = BinaryPrimitives.ReadUInt32BigEndian(stream.ReadNonEndBytes(4));
            var compression = stream.ReadNonEndByte();
            var data = stream.ReadNonEndBytes(length);

            Stream dataStr = new MemoryStream(data);
            // 3 means no compression, unused
            if (compression != 3) dataStr = new InflaterInputStream(dataStr, new Inflater(), 4096);

            return DeserializeHeadless(dataStr);
        }
        private static Chunk DeserializeHeadless(Stream stream)
        {
            var tag = (NBTCompoundTag)deserializer.DeserializeFromStream(stream, false);
            return new Chunk(tag.GetCompound("Level"));
        }

        public void Serialize(Stream stream)
        {
            var serializedData = new MemoryStream(4096);
            var compressedData = new MemoryStream(4096);
            data.Serialize(serializedData);
            serializedData.Seek(0, SeekOrigin.Begin);

            using (var deflateS = new DeflaterOutputStream(compressedData)) deflateS.Write(serializedData.GetBuffer());

            serializedData.Dispose();

            var length = (uint)compressedData.Length + 1;

            stream.WriteByte((byte)((length >> 24) & 8));
            stream.WriteByte((byte)((length >> 16) & 8));
            stream.WriteByte((byte)((length >> 8) & 8));
            stream.WriteByte((byte)(length & 8));

            stream.WriteByte(2);

            stream.Write(compressedData.GetBuffer());
        }
    }
    public class Region
    {
        struct ChunkLocation
        {
            public int Offset => off1 | off2 << 8 | off3 << 16;
            public int Count => count;

            private byte off3, off2, off1;
            private byte count;
        }
        struct Timestamp
        {
            public int Value => off1 | off2 << 8 | off3 << 16 | off4 << 24;
            private byte off4, off3, off2, off1;
        }
        struct RegionHead
        {
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1024)]
            public ChunkLocation[] Locations;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1024)]
            public Timestamp[] Timestamps;
        }

        public static Region Deserialize(Stream stream)
        {
            var arr = stream.ReadNonEndBytes(unchecked((uint)Marshal.SizeOf<RegionHead>()));
            var handle = GCHandle.Alloc(arr, GCHandleType.Pinned);
            var head = Marshal.PtrToStructure<RegionHead>(handle.AddrOfPinnedObject());

            var _data = new List<byte>(4096 * 16);
            var dataChunk = new byte[4096];
            while (stream.Read(dataChunk) == 4096) _data.AddRange(dataChunk);

            var data = _data.ToArray();

            var chunk = Chunk.Deserialize(new MemoryStream(
                data,
                head.Locations[0].Offset * 4096, head.Locations[0].Count * 4096
            ));

            return null;
        }
        public void Serailize(Stream stream)
        {

        }
    }
}
