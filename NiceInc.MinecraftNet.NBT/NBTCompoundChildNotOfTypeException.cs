using System;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTCompoundChildNotOfTypeException : NBTException
    {
        public NBTCompoundChildNotOfTypeException() : base("The NBT tag's value is not of the specified type.") { }
        public NBTCompoundChildNotOfTypeException(Type type)
            : base($"The NBT tag's value is not of the type {type.FullName}") { }
    }
}
