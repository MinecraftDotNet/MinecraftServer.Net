using System.IO;
using System.Linq;

namespace NiceInc.MinecraftNet.NBT
{
    public record NBTTagHead(string Name, TagType Type)
    {
        public void Serialize(Stream stream)
        {
            Serialize(Name, Type, stream);
        }
        public static void Serialize(string name, TagType type, Stream stream)
        {
            stream.WriteByte((byte)type);
            stream.WriteByte(unchecked((byte)(name.Length >> 8)));
            stream.WriteByte(unchecked((byte)name.Length));
            stream.Write(name.Select(v => (byte)v).ToArray());
        }
    }
}
