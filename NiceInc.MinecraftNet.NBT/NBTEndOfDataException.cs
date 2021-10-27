namespace NiceInc.MinecraftNet.NBT
{
    public class NBTEndOfDataException : NBTException
    {
        public NBTEndOfDataException() : this("Unexpected end of NBT data.") { }
        public NBTEndOfDataException(string message) : base(message) { }
    }
}
