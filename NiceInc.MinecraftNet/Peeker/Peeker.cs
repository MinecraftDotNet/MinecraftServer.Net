using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceInc.MinecraftNet.Peeker
{
    public class PeekerEndedException : Exception
    {
        public PeekerEndedException(): base("Enexpected peeker end.")
        {
        }
    }
    public static class Peeker
    {
        public static void ThrowIfEnd<T>(this IPeeker<T> peeker)
        {
            if (peeker.Ended) throw new PeekerEndedException();
        }
    }
}
