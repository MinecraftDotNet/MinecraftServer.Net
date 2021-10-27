using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public interface ITagDeserializer
    {
        ITag Deserialize(Stream stream, NBTDeserializer deserializer);
    }
}
