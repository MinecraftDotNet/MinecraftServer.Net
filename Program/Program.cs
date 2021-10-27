using NiceInc.MinecraftNet.NBT;
using NiceInc.MinecraftNet.Save;
using System.IO;

namespace Program
{
    class Program
    {

        static void Main(string[] args)
        {
            var deserializer = NBTDeserializer.Default();
            using (var file = File.OpenRead(@"D:/.minecraft/saves/survival/region/r.-4.0.mca")) {
                Region.Deserialize(file);
            }
        }
    }
}
