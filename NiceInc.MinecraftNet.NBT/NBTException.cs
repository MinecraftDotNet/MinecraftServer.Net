using System;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTException : Exception
    {
        public NBTException() : this("An NBT error was thrown.") { }
        public NBTException(string message) : base(message) { }
    }
}
