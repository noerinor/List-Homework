using System;
using System.Collections.Generic;

namespace CustomCollections
{
    public static class Extensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
        {
            int skipped = 0;
            foreach (var item in source)
            {
                if (skipped++ >= count)
                    yield return item;
            }
        }

        public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            bool yielding = false;
            foreach (var item in source)
            {
                if (!yielding && !predicate(item))
                    yielding = true;

                if (yielding)
                    yield return item;
            }
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            int taken = 0;
            foreach (var item in source)
            {
                if (taken++ < count)
                    yield return item;
                else
                    break;
            }
        }

        public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
                else
                    break;
            }
        }

        public static T First<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    return item;
            }
            throw new InvalidOperationException("no matching element found.");
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    return item;
            }
            return default;
        }

        public static T Last<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            T result = default;
            bool found = false;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result = item;
                    found = true;
                }
            }

            if (found)
                return result;
            throw new InvalidOperationException("no matching element found.");
        }

        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            T result = default;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result = item;
                }
            }

            return result;
        }

        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> SelectMany<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
        {
            foreach (var item in source)
            {
                foreach (var subItem in selector(item))
                {
                    yield return subItem;
                }
            }
        }

        public static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (!predicate(item))
                    return false;
            }
            return true;
        }

        public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    return true;
            }
            return false;
        }

        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            var result = new List<T>();
            foreach (var item in source)
            {
                result.Add(item);
            }
            return result;
        }

        public static T[] ToArray<T>(this IEnumerable<T> source)
        {
            var list = source.ToList();
            return list.ToArray();
        }
    }
}
