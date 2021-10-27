using System.IO;

namespace NiceInc.MinecraftNet.NBT
{
    public static class StreamExtentions
    {
        public static byte ReadNonEndByte(this Stream stream)
        {
            int i = stream.ReadByte();
            if (i == -1) throw new NBTEndOfDataException();
            return (byte)i; 
        }
        public static byte[] ReadNonEndBytes(this Stream stream, ulong n)
        {
            var buffer = new byte[n];
            for (ulong i = 0; i < n; i++) {
                int val = stream.ReadByte();
                if (val == -1) throw new NBTEndOfDataException();
                buffer[i] = (byte)val;
            }

            return buffer;
        }
    }
}
