namespace NiceInc.MinecraftNet.NBT
{
    public class NBTTagNotOfListTypeException : NBTException
    {
        public NBTTagNotOfListTypeException() : base("The element specified was not of the list's type.") { }
        public NBTTagNotOfListTypeException(TagType listType, TagType elementType)
            : base($"The element was of type {elementType}, while the list was of type {listType}.") { }
    }
}
