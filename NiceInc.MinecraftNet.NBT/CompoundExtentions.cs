using System.Collections.Generic;

namespace NiceInc.MinecraftNet.NBT
{
    public static class CompoundExtentions
    {
        public static T GetValue<T>(this NBTCompoundTag compound, string name)
        {
            if (compound[name] is IValueTag<T> tag) return tag.Value;
            throw new NBTCompoundChildNotOfTypeException(typeof(T));
        }
        public static NBTCompoundTag GetCompound(this NBTCompoundTag compound, string name)
        {
            if (compound[name] is NBTCompoundTag child) return child;
            throw new NBTCompoundChildNotOfTypeException(typeof(NBTCompoundTag));
        }
        public static NBTListTag GetList(this NBTCompoundTag compound, string name)
        {
            if (compound[name] is NBTListTag child) return child;
            throw new NBTCompoundChildNotOfTypeException(typeof(NBTListTag));
        }
        public static NBTArrayTag<T> GetArray<T>(this NBTCompoundTag compound, string name) where T : unmanaged
        {
            if (compound[name] is NBTArrayTag<T> child) return child;
            throw new NBTCompoundChildNotOfTypeException(typeof(NBTArrayTag<>));
        }
        public static T GetTag<T>(this NBTCompoundTag compound, string name) where T : ITag
        {
            if (compound[name] is T tag) return tag;
            else throw new NBTCompoundChildNotOfTypeException(typeof(T));
        }
        public static bool TryGetValue<T>(this NBTCompoundTag compound, string name, out T value)
        {
            try {
                value = compound.GetValue<T>(name);
                return true;
            }
            catch (NBTCompoundChildNotOfTypeException) {
                value = default;
                return false;
            }
        }
        public static bool TryGetCompound(this NBTCompoundTag compound, string name, out NBTCompoundTag value)
        {
            try {
                value = compound.GetCompound(name);
                return true;
            }
            catch (NBTCompoundChildNotOfTypeException) {
                value = null;
                return false;
            }
        }
        public static bool TryGetList(this NBTCompoundTag compound, string name, out NBTListTag value)
        {
            try {
                value = compound.GetList(name);
                return true;
            }
            catch (NBTCompoundChildNotOfTypeException) {
                value = null;
                return false;
            }
        }
        public static bool TryGetArray<T>(this NBTCompoundTag compound, string name, out NBTArrayTag<T> value)
            where T : unmanaged
        {
            try {
                value = compound.GetArray<T>(name);
                return true;
            }
            catch (NBTCompoundChildNotOfTypeException) {
                value = null;
                return false;
            }
        }

        public static void Set(this NBTCompoundTag compound, string name, ICollection<ITag> value)
        {
            if (compound[name] is NBTListTag tag) {
                tag.Clear();
                foreach (var item in value) tag.Add(item);
            }
            else {
                compound[name] = new NBTListTag(TagType.End);
                compound.Set(name, value);
            }
        }

        public static void Set(this NBTCompoundTag compound, string name, sbyte[] value)
        {
            if (compound[name] is NBTArrayTag<sbyte> tag) {
                tag.Elements.Clear();
                tag.Elements.AddRange(value);
            }
            else compound[name] = new NBTArrayTag<sbyte>(value, TagType.ByteArray);
        }
        public static void Set(this NBTCompoundTag compound, string name, int[] value)
        {
            if (compound[name] is NBTArrayTag<int> tag) {
                tag.Elements.Clear();
                tag.Elements.AddRange(value);
            }
            else compound[name] = new NBTArrayTag<int>(value, TagType.IntArray);
        }
        public static void Set(this NBTCompoundTag compound, string name, long[] value)
        {
            if (compound[name] is NBTArrayTag<long> tag) {
                tag.Elements.Clear();
                tag.Elements.AddRange(value);
            }
            else compound[name] = new NBTArrayTag<long>(value, TagType.LongArray);
        }

        public static void Set(this NBTCompoundTag compound, string name, sbyte value)
            => Set(compound, name, value, TagType.Byte);
        public static void Set(this NBTCompoundTag compound, string name, short value)
            => Set(compound, name, value, TagType.Short);
        public static void Set(this NBTCompoundTag compound, string name, int value)
            => Set(compound, name, value, TagType.Int);
        public static void Set(this NBTCompoundTag compound, string name, long value)
            => Set(compound, name, value, TagType.Long);
        public static void Set(this NBTCompoundTag compound, string name, float value)
            => Set(compound, name, value, TagType.Float);
        public static void Set(this NBTCompoundTag compound, string name, double value)
            => Set(compound, name, value, TagType.Double);

        public static void Set(this NBTCompoundTag compound, string name, string value)
        {
            if (compound[name] is IValueTag<string> tag) tag.Value = value;
            else compound[name] = new NBTStringTag(value);
        }

        private static void Set<T>(this NBTCompoundTag compound, string name, T value, TagType type) where T : unmanaged
        {
            if (compound[name] is IValueTag<T> tag) tag.Value = value;
            else compound[name] = new NBTValueTag<T>(value, type);
        }
    }
}
