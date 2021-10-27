using NiceInc.MinecraftNet.Modding;
using System;
using System.Collections.Generic;

namespace TestMod
{
    public class TestMod : IMod
    {
        public void Load(ModCollection deps)
        {
            Console.WriteLine("I've been loaded!");
        }

        public void Unload()
        {
            Console.WriteLine("igh imma head out");
        }
    }
}
