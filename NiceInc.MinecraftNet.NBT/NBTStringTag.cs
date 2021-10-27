using System.IO;
using System.Linq;

namespace NiceInc.MinecraftNet.NBT
{
    public class NBTStringTag : IValueTag<string>
    {
        public string Value { get; set; }
        public TagType Type => TagType.String;

        public void Serialize(Stream stream)
        {
            stream.WriteByte(unchecked((byte)(Value.Length >> 8)));
            stream.WriteByte(unchecked((byte)Value.Length));
            stream.Write(Value.Select(v => (byte)v).ToArray());
        }

        public override string ToString() => Value;

        public NBTStringTag(string value)
        {
            Value = value;
        }
    }
}
