using System.Collections.Generic;
using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTArrayTag<T> : IArrayTag<T> where T : unmanaged
    {
        public List<T> Elements { get; }
        public TagType Type { get; }
        public int Length => Elements.Count;
        public T this[int i] {
            get => Elements[i];
            set => Elements[i] = value;
        }

        public void Serialize(Stream stream)
        {
            new NBTValueTag<int>(Elements.Count).Serialize(stream);

            foreach (var item in Elements) {
                new NBTValueTag<T>(item).Serialize(stream);
            }
        }

        public NBTArrayTag(T[] elements, TagType type)
        {
            Elements = new List<T>(elements);
            Type = type;
        }
    }
}
