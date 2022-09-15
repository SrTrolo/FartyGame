using System.Collections.Generic;
using UnityEngine;


namespace Stracker.ListSystem
{
    static public class ListSystem
    {
        //list = ListSystem.Method(list,...
        //array = ListSystem.Method(array,...

        public static List<T> Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
            return list;
        }
        public static T[] Move<T>(this T[] array, int oldIndex, int newIndex)
        {
            List<T> tempList = new List<T>(array);

            T item = tempList[oldIndex];
            tempList.RemoveAt(oldIndex);
            tempList.Insert(newIndex, item);
            return tempList.ToArray();
        }
        public static List<T> Swap<T>(this List<T> list, int indexA, int indexB)
        {
            T item = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = item;
            return list;
        }
        public static T[] Swap<T>(this T[] array, int indexA, int indexB)
        {
            List<T> tempList = new List<T>(array);

            T item = tempList[indexA];
            tempList[indexA] = tempList[indexB];
            tempList[indexB] = item;
            return tempList.ToArray();
        }
        public static List<T> Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
            return list;
        }
        public static T[] Shuffle<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                T temp = array[i];
                int randomIndex = Random.Range(i, array.Length);
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
            return array;
        }
        public static bool HasValue<T>(this List<T> list, T value)
        {
            return list.Contains(value);
        }
        public static bool HasValue<T>(this T[] array, T value)
        {
            List<T> tempList = new List<T>(array);
            return tempList.Contains(value);
        }
        /// <summary>
        /// Comprueba si contiene el valor en la clase
        /// <example>
        /// <code>
        ///    ListSystem.HasValue(list, x => x.param == "Value")
        /// </code>
        /// </example>
        /// </summary>
        public static bool HasValue<T>(List<T> list, System.Predicate<T> match)
        {
            return list.Exists(match);
        }
        /// <summary>
        /// Comprueba si contiene el valor en la clase
        /// <example>
        /// <code>
        ///    ListSystem.HasValue(array, x => x.param == "Value")
        /// </code>
        /// </example>
        /// </summary>
        public static bool HasValue<T>(T[] array, System.Predicate<T> match)
        {
            List<T> tempList = new List<T>(array);
            return tempList.Exists(match);
        }

        public static int GetIndexValue<T>(this List<T> list, T value)
        {
            return list.IndexOf(value);
        }
        public static int GetIndexValue<T>(this T[] array, T value)
        {
            List<T> tempList = new List<T>(array);
            return tempList.IndexOf(value);
        }
        /// <summary>
        /// Devuelve el indice de la lista de una clase en base al valor
        /// <example>
        /// <code>
        ///    ListSystem.GetIndexValue(list, x => x.param == "Value")
        /// </code>
        /// </example>
        /// </summary>
        public static int GetIndexValue<T>(this List<T> list, System.Predicate<T> match)
        {
            return list.FindIndex(match);
        }
        /// <summary>
        /// Devuelve el indice del array de una clase en base al valor
        /// <example>
        /// <code>
        ///    ListSystem.GetIndexValue(array, x => x.param == "Value")
        /// </code>
        /// </example>
        /// </summary>
        public static int GetIndexValue<T>(T[] array, System.Predicate<T> match)
        {
            List<T> tempList = new List<T>(array);
            return tempList.FindIndex(match);
        }

        /// <summary>
        /// Ordena y devuelve una lista
        /// </summary>
        public static List<T> SortList<T>(this List<T> list)
        {
            list.Sort();
            return list;
        }
        /// <summary>
        /// Ordena y devuelve un array
        /// </summary>
        public static T[] SortList<T>(this T[] array)
        {
            List<T> tempList = new List<T>(array);
            tempList.Sort();
            array = tempList.ToArray();
            return array;
        }
        /// <summary>
        /// Ordena y devuelve una lista en vase a un parametro
        /// <example>
        /// <code>
        ///    List = List.SortList(<strong>x => x.param</strong>);
        /// </code>
        /// </example>
        /// </summary>
        public static List<T> SortList<T, U>(this List<T> list, System.Func<T, U> expression) where U : System.IComparable<U>
        {
            list.Sort((x, y) => expression.Invoke(x).CompareTo(expression.Invoke(y)));
            return list;
        }
        /// <summary>
        /// Ordena y devuelve un array en vase a un parametro
        /// <example>
        /// <code>
        ///     Array = Array.SortList(<strong>x => x.param</strong>);
        /// </code>
        /// </example>
        /// </summary>
        public static T[] SortList<T, U>(this T[] array, System.Func<T, U> expression) where U : System.IComparable<U>
        {
            List<T> tempList = new List<T>(array);
            tempList.Sort((x, y) => expression.Invoke(x).CompareTo(expression.Invoke(y)));
            array = tempList.ToArray();
            return array;
        }
    }
}
