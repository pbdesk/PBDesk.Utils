using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBDesk.Utils
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Details at http://solutionizing.net/2009/07/23/using-idisposables-with-linq/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<T> Kill<T>(this T obj) where T : IDisposable
        {
            try
            {
                yield return obj;
            }
            finally
            {
                if (obj != null)
                    obj.Dispose();
            }
        }

        public static void ForEach<T>(this IEnumerable<T> coll, Action<T> function)
        {
            IEnumerator<T> en = coll.GetEnumerator();
            while (en.MoveNext())
                function(en.Current);
        }
    }
}
