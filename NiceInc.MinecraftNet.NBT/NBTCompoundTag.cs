using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTCompoundTag : ITag, IDictionary<string, ITag>
    {
        private SortedDictionary<string, ITag> children = new SortedDictionary<string, ITag>();
        public TagType Type => TagType.Compound;

        public ITag this[string key] {
            get => children[key];
            set => children[key] = value;
        }

        public void Serialize(Stream stream)
        {
            foreach (var child in children) {
                new NBTTagHead(child.Key, child.Value.Type).Serialize(stream);
                child.Value.Serialize(stream);
            }

            stream.WriteByte(0);
        }

        public ICollection<string> Names => children.Keys;
        public ICollection<ITag> Children => children.Values;
        public int Count => children.Count;
        public bool IsReadOnly => false;

        ICollection<string> IDictionary<string, ITag>.Keys => Names;
        ICollection<ITag> IDictionary<string, ITag>.Values => Children;

        public void Add(string key, ITag value)
        {
            throw new System.NotImplementedException();
        }
        public bool Contains(string name) => children.ContainsKey(name);
        public bool Remove(string name) => children.Remove(name);
        public bool TryGetChild(string key, out ITag value) => children.TryGetValue(key, out value);
        public void Clear() => children.Clear();

        bool ICollection<KeyValuePair<string, ITag>>.Contains(KeyValuePair<string, ITag> item)
            => TryGetChild(item.Key, out var res) && res == item.Value;
        void ICollection<KeyValuePair<string, ITag>>.CopyTo(KeyValuePair<string, ITag>[] array, int arrayIndex)
            => children.ToArray().CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<string, ITag>>.Remove(KeyValuePair<string, ITag> item)
            => TryGetChild(item.Key, out var res) && res == item.Value && children.Remove(item.Key);
        public IEnumerator<KeyValuePair<string, ITag>> GetEnumerator()
            => children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => children.GetEnumerator();
        void ICollection<KeyValuePair<string, ITag>>.Add(KeyValuePair<string, ITag> item)
            => children.Add(item.Key, item.Value);
        bool IDictionary<string, ITag>.ContainsKey(string key) => Contains(key);
        bool IDictionary<string, ITag>.TryGetValue(string key, out ITag value)
            => TryGetChild(key, out value);

        public NBTCompoundTag() { }

        public override bool Equals(object obj)
        {
            if (obj is NBTCompoundTag tag) {
                if (tag.Count != Count) return false;
                return this.SequenceEqual(tag, (a, b) => a.Key == b.Key && a.Value.Equals(b.Value));
            }
            return false;

        }
    }
}
