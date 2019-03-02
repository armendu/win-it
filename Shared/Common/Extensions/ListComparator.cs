using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class ListComparator
    {
        public static bool EqualsAll<T>(this IList<T> a, IList<T> b)
        {
            if (a == null || b == null)
                return (a == null && b == null);
        
            if (a.Count != b.Count)
                return false;
        
            return a.SequenceEqual(b);
        }

        public static bool ArePermutations<T>(this IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            var l1 = list1.ToLookup(t => t);
            var l2 = list2.ToLookup(t => t);

            return l1.Count == l2.Count
                   && l1.All(group => l2.Contains(group.Key) && l2[group.Key].Count() == group.Count());
        }
    }
}