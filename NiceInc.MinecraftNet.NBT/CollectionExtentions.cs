using System.Collections.Generic;

namespace NiceInc.MinecraftNet.NBT
{
    public static class CollectionExtentions
    {
        public static bool SequenceEqual<T>(
            this IEnumerable<T> a, IEnumerable<T> b,
            DefaultEqualityComparer<T>.EqualsFunction equals)
        {
            bool aEnded = false, bEnded = false;
            using (var enumA = a.GetEnumerator())
            using (var enumB = b.GetEnumerator())
                while ((aEnded = enumA.MoveNext()) && (bEnded = enumB.MoveNext())) {
                    if (!equals(enumA.Current, enumB.Current)) return false;
                }

            return aEnded && bEnded;
        }
    }
}
