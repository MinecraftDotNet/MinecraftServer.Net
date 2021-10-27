using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTArrayTagDeserializer<T> : ITagDeserializer where T : unmanaged
    {
        private readonly TagType tagType;
        private static NBTValueTagDeserializer<T> valDeserializer = new NBTValueTagDeserializer<T>(TagType.End);

        public ITag Deserialize(Stream stream, NBTDeserializer deserializer)
        {
            int length = ((IValueTag<int>)deserializer.DeserializeSingleHeadless(stream, TagType.Int)).Value;
            var data = new T[length];

            for (int i = 0; i < length; i++)
                data[i] = ((IValueTag<T>)valDeserializer.Deserialize(stream, deserializer)).Value;

            return new NBTArrayTag<T>(data, tagType);
        }

        public NBTArrayTagDeserializer(TagType tagType)
        {
            this.tagType = tagType;
        }
    }
}
