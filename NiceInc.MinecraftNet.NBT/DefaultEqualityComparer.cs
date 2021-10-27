using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NiceInc.MinecraftNet.NBT
{
    public class DefaultEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y) => EqualsFunc(x, y);
        public int GetHashCode([DisallowNull] T obj) => HashFunc(obj);

        public delegate bool EqualsFunction(T a, T b);
        public delegate int HashFunction(T obj);

        public EqualsFunction EqualsFunc { get; set; }
        public HashFunction HashFunc { get; set; }

        public DefaultEqualityComparer()
        {
            EqualsFunc = (a, b) => a.Equals(b);
            HashFunc = obj => obj.GetHashCode();
        }
        public DefaultEqualityComparer(EqualsFunction equals)
        {
            EqualsFunc = equals;
            HashFunc = obj => obj.GetHashCode();
        }
        public DefaultEqualityComparer(EqualsFunction equals, HashFunction hash)
        {
            EqualsFunc = equals;
            HashFunc = hash;
        }
    }
}
