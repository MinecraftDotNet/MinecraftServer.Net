using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceInc.MinecraftNet.World
{
    public static class ObjectExtention
    {
        public static OutT SelectSingle<T, OutT>(this T val, Func<T, OutT> action)
        {
            return action(val);
        }
        public static T SelectSingle<T>(this T val, Action<T> action)
        {
            action(val);
            return val;
        }
    }
}
