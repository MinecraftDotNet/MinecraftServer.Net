using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceInc.MinecraftNet.NBT
{
    public static class ListExtensions
    {
        public static T GetValue<T>(this NBTListTag list, int i) where T : unmanaged
        {
            if (list[i] is IValueTag<T> tag) return tag.Value;
            throw new Exception($"The type wasn't {nameof(IValueTag<T>)}");
        }
        public static NBTArrayTag<T> GetArray<T>(this NBTListTag list, int i) where T : unmanaged
        {
            if (list[i] is NBTArrayTag<T> tag) return tag;
            throw new Exception($"The type wasn't {nameof(NBTArrayTag<T>)}");
        }
        public static NBTCompoundTag GetCompound(this NBTListTag list, int i)
        {
            if (list[i] is NBTCompoundTag tag) return tag;
            throw new Exception($"The type wasn't {nameof(NBTCompoundTag)}");
        }
        public static NBTListTag GetList(this NBTListTag list, int i)
        {
            if (list[i] is NBTListTag tag) return tag;
            throw new Exception($"The type wasn't {nameof(NBTListTag)}");
        }

        public static void Set<T>(this NBTListTag list, int i, T value)
        {
            if (typeof(T) == typeof(NBTListTag)) list[i] = (NBTListTag)(object)value;
            if (typeof(T) == typeof(NBTCompoundTag)) list[i] = (NBTCompoundTag)(object)value;
            if (list[i] is IValueTag<T> tag) tag.Value = value;
            throw new ArgumentException("The type is not recognised", "T");
        }
        public static IEnumerator<T> GetEnumerator<T>(this NBTListTag list)
        {
            if (typeof(T) == typeof(NBTListTag)) return (IEnumerator<T>)list.Select(v => (NBTListTag)v).GetEnumerator();
            if (typeof(T) == typeof(NBTCompoundTag)) return (IEnumerator<T>)list.Select(v => (NBTCompoundTag)v).GetEnumerator();
            return list.Select(v => ((IValueTag<T>)v).Value).GetEnumerator();
        }

        public static void Add(this NBTListTag list, sbyte value) => list.Add(TagType.Byte, value);
        public static void Add(this NBTListTag list, short value) => list.Add(TagType.Short, value);
        public static void Add(this NBTListTag list, int value) => list.Add(TagType.Int, value);
        public static void Add(this NBTListTag list, long value) => list.Add(TagType.Long, value);
        public static void Add(this NBTListTag list, float value) => list.Add(TagType.Float, value);
        public static void Add(this NBTListTag list, double value) => list.Add(TagType.Double, value);
        public static void Add(this NBTListTag list, string value) => list.Add(new NBTStringTag(value));

        private static void Add<T>(this NBTListTag list, TagType type, T value) where T : unmanaged
            => list.Add(new NBTValueTag<T>(value, type));
    }
}
