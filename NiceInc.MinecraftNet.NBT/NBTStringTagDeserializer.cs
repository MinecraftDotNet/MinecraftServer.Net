using System.IO;
using System.Linq;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTStringTagDeserializer : ITagDeserializer
    {
        public ITag Deserialize(Stream stream, NBTDeserializer deserializer)
        {
            int length =
                stream.ReadNonEndByte() << 8 |
                stream.ReadNonEndByte();

            var data = stream.ReadNonEndBytes((uint)length);
            return new NBTStringTag(new string(data.Select(v => (char)v).ToArray()));
        }
    }
}
