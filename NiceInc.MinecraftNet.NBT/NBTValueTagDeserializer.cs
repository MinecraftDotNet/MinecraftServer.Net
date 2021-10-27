using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTValueTagDeserializer<T> : ITagDeserializer where T : unmanaged
    {
        private readonly TagType type;
        private readonly bool reverse;

        public ITag Deserialize(Stream stream, NBTDeserializer deserializer)
        {
            unsafe {
                var data = stream.ReadNonEndBytes((uint)sizeof(T));
                if (reverse) Array.Reverse(data);
                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                return new NBTValueTag<T>(Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject()), type, reverse);
            }
        }

        public NBTValueTagDeserializer(TagType type, bool reverse = true)
        {
            this.type = type;
            this.reverse = reverse;
        }
    }
}
