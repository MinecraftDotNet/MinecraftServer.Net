using System;

namespace NiceInc.MinecraftNet.World
{
    public class IdentifierDoesntExistException : Exception
    {
        public IdentifierDoesntExistException(Identifier identifier) : base($"The identifier '{identifier}' doesn't exist.")
        {
        }
        public IdentifierDoesntExistException(Identifier identifier, string details) : base($"The identifier '{identifier}' doesn't exist: {details}.")
        {
        }
    }
}
