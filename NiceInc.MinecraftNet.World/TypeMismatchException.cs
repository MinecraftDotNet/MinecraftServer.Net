using System;

namespace NiceInc.MinecraftNet.World
{
    public class TypeMismatchException : Exception
    {
        public TypeMismatchException(Type a, Type b): base ($"The type {a.FullName} isn't an instance of {b.FullName}.") { }
    }
}
