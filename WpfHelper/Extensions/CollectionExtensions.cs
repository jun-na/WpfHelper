using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.ObjectModel
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// コレクションを追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Collection<T> AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }

        /// <summary>
        /// 値が異なればクリアして設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Collection<T> SetRange<T>(this Collection<T> collection, IEnumerable<T> items, Func<T, T, bool>? equalFunc = null)
        {
            if (equalFunc != null && collection.Count() == items.Count())
            {
                var isNotEqual = false;
                for (var i = 0; i < collection.Count; i++)
                {
                    if (!equalFunc(collection[i], items.ElementAt(i)))
                    {
                        isNotEqual = true;
                        break;
                    }
                }
                if (!isNotEqual)
                {
                    return collection;
                }
            }

            collection.Clear();
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
