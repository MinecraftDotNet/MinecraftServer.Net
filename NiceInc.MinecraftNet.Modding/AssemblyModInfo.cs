using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Reflection;

namespace NiceInc.MinecraftNet.Modding
{
    public class AssemblyModInfo : ModInfo
    {
        [JsonRequired]
        public Dictionary<string, string> EntryPoints { get; init; }
    }
}
