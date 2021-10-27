namespace NiceInc.MinecraftNet.NBT
{
    public class NBTUnknownTagTypeException : NBTException
    {
        public NBTUnknownTagTypeException() : base("An unknown tag type was encountered.") { }
        public NBTUnknownTagTypeException(byte type) : base($"An unknown tag type 0x{type:X2} was encountered.") { }
    }
}
