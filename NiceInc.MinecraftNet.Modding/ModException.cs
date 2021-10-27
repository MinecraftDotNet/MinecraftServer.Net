using System;

namespace NiceInc.MinecraftNet.Modding
{
    public class ModException : Exception
    {
        public ModException(string message) : base(message)
        {
        }
    }
}
