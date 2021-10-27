using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceInc.MinecraftNet.Modding
{
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException(IEnumerable<Mod> mods)
            : base($"The mods {string.Join(", ", mods.Select(v => $"{v.Info.Name} {v.Info.Version}"))} form a circular dependency.") { }
    }
}
