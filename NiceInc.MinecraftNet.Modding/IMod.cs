using System.Collections;
using System.Collections.Generic;

namespace NiceInc.MinecraftNet.Modding
{
    public interface IMod
    {
        void Load(ModCollection dependencies);
        void Unload();
    }
}
