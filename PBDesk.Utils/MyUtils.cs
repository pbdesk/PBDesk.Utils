/*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*
 *~*  www.PBDesk.com 2010/2011                                                      
 *~*  
 *~*  Project : PBDesk.Utils                        Namespace : PBDesk.Utils
 *~*  File : Myutils.cs                                 Class : MyUtils
 *~*  
 *~*  Author : Pinal Bhatt (pinal.bhatt@PBDesk.com)
 *~*  Version History
 *~*            Ver. 1.0     Date 12-Oct-2010       Pinal Bhatt
 *~*            Ver. 2.0     Date 21-Jun-2011       Pinal Bhatt
 *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace PBDesk.Utils
{
    public class MyUtils
    {
        private static Random RndGenerator = new Random();

        #region Array/Shuffle Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[] ShuffleArray<T>(T[] arr)
        {
            List<KeyValuePair<int, T>> list = new List<KeyValuePair<int, T>>();
            foreach (T item in arr)
            {
                list.Add(new KeyValuePair<int, T>(RndGenerator.Next(), item));
            }
            var sorted = from listItem in list orderby listItem.Key select listItem;

            T[] result = new T[arr.Length];
            int i = 0;
            foreach (KeyValuePair<int, T> pair in sorted)
            {
                result[i] = pair.Value;
                i++;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="startValue"></param>
        /// <returns></returns>
        public static int[] GetShuffleArr(int size, int startValue = 0)
        {

            if (size > 0)
            {

                int[] arr = new int[size];
                for (int i = 0, j = startValue; i < size; i++, j++)
                {
                    arr[i] = j;
                }
                return ShuffleArray<int>(arr);

            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="startValue"></param>
        /// <returns></returns>
        public static string GetShuffleCSV(int size, int startValue = 0)
        {
            int[] arr = GetShuffleArr(size, startValue);
            if (arr != null && arr.Length > 0)
                return String.Join(",", arr);
            else
                return string.Empty;
        }

        #endregion
    }
}
