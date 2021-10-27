using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTCompoundTagDeserializer : ITagDeserializer
    {
        public ITag Deserialize(Stream stream, NBTDeserializer deserializer)
        {
            var tag = new NBTCompoundTag();
            while (true) {
                var headedTag = deserializer.DeserializeSingle(stream);
                if (headedTag == null) break;
                tag[headedTag.Head.Name] = headedTag.Tag;
            }

            return tag;
        }
    }
}
