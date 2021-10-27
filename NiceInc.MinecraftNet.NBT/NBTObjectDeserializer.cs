using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceInc.MinecraftNet.NBT
{
    public class UndeserializableTagTypeException : Exception
    {
        public UndeserializableTagTypeException(Type type) : base("The tag type " + type.Name + " couldn't be deserialized.")
        {
        }
    }
    public static class NBTObjectDeserializer
    {
        public static object Deserialize(IValueTag tag) => tag.Value;
        public static string Deserialize(NBTStringTag tag) => tag.Value;
        public static IEnumerable Deserialize(IArrayTag tag)
        {
            var data = new object[tag.Length];
            for (int i = 0; i < tag.Length; i++) data[i] = tag[i];
            return data;
        }

        public static IEnumerable Deserialize(NBTListTag tag)
        {
            ArrayList list = new ArrayList();
            foreach (var item in tag) {
                list.Add(Deserialize(item));
            }
            return list;
        }
        public static object Desrialize(NBTCompoundTag tag, Type t)
        {
            Activator.CreateInstance(t);
        }
        public static object Deserialize(ITag tag)
        {
            if (tag is IValueTag value) return Deserialize(value);
            if (tag is IArrayTag array) return Deserialize(array);
            if (tag is NBTListTag list) return Deserialize(list);
            if (tag is NBTStringTag str) return Deserialize(str);
            if (tag is NBTCompoundTag compound) return Deserialize(compound);
            throw new UndeserializableTagTypeException(tag.GetType());
        }
    }
}
