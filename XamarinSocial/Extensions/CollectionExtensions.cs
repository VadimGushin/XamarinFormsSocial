using System.Collections;

namespace XamarinSocial.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange(this IList enumerable, IEnumerable range)
        {
            if (range == null)
            {
                return;
            }
            foreach (object item in range)
            {
                enumerable.Add(item);
            }
        }
    }
}
