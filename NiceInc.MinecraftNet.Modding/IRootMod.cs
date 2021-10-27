using NiceInc.MinecraftNet.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceInc.MinecraftNet.Modding
{
    public interface IRootMod : IMod
    {
        Config CliArguments { get; }
        ModLoader ModLoader { get; }
    }
}
