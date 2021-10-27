using System;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTInvalidAssignmentType : NBTException
    {
        public NBTInvalidAssignmentType() : base("The NBT value tag's value coud not be set to the given value.") { }
        public NBTInvalidAssignmentType(Type type)
            : base($"The NBT value tag's value coud not be set to a value of type {type.FullName}.") { }
        public NBTInvalidAssignmentType(object value)
            : base($"The NBT value tag's value coud not be set to {value} ({value.GetType()}).") { }
    }
}
