using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTListTag : ITag, IList<ITag>
    {
        private List<ITag> items = new List<ITag>();

        public ITag this[int index] {
            get => items[index];
            set {
                if (value.Type != ChildrenType) throw new NBTTagNotOfListTypeException(ChildrenType, value.Type);
                items[index] = value;
            }
        }

        public TagType Type => TagType.List;
        public TagType ChildrenType { get; private set; }

        public int Count => items.Count;
        public bool IsReadOnly => false;
        public void Add(ITag item) => Insert(Count - 1, item);
        public void Clear() => items.Clear();
        public bool Contains(ITag item) => items.Contains(item);
        public void CopyTo(ITag[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);
        public IEnumerator<ITag> GetEnumerator() => items.GetEnumerator();
        public int IndexOf(ITag item) => items.IndexOf(item);
        public void Insert(int index, ITag item)
        {
            if (Count == 0) ChildrenType = item.Type;
            if (item.Type != ChildrenType) throw new NBTTagNotOfListTypeException(ChildrenType, item.Type);
            items.Insert(index, item);
        }
        public bool Remove(ITag item) => items.Remove(item);
        public void RemoveAt(int index) => items.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Serialize(Stream stream)
        {
            stream.WriteByte((byte)ChildrenType);
            new NBTValueTag<int>(Count, TagType.Int).Serialize(stream);
            foreach (var item in items) item.Serialize(stream);
        }
        public NBTListTag(TagType childrenType)
        {
            ChildrenType = childrenType;
        }
        public NBTListTag(TagType childrenType, ITag[] items)
        {
            this.items = new List<ITag>(items);
            ChildrenType = childrenType;
        }
    }
}
