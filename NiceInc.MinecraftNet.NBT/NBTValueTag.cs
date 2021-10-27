using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTValueTag<T> : ITag, IValueTag<T> where T : unmanaged
    {
        private T value = default;
        private readonly bool reverse;

        public T Value {
            get => value;
            set => this.value = value;
        }
        public TagType Type { get; }

        public NBTValueTag(T value, TagType type = TagType.End, bool reverse = true)
        {
            Value = value;
            Type = type;
            this.reverse = reverse;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public void Serialize(Stream stream)
        {
            unsafe {
                fixed (T* _ptr = &value) {
                    int size = sizeof(T);
                    byte* ptr = (byte*)_ptr;

                    if (reverse)
                        for (int i = size - 1; i >= 0; i--) stream.WriteByte(ptr[i]);
                    else
                        for (int i = 0; i < size; i++) stream.WriteByte(ptr[i]);
                }
            }
        }
    }
}
