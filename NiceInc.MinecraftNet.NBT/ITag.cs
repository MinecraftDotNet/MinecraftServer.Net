using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public interface ITag
    {
        TagType Type { get; }
        void Serialize(Stream stream);
    }
}
