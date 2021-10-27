using NiceInc.MinecraftNet.Peeker;
using System.Collections.Generic;

namespace NiceInc.MinecraftNet.Peeker
{
    public static class Enumerable
    {
        public static IPeeker<T> GetPeeker<T>(this IEnumerable<T> enumerable) => new EnumerablePeeker<T>(enumerable);
    }
}
