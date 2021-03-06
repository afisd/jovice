﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aphysoft.Share
{
    public static class List
    {
        public static bool Exists<T>(this List<T> list, T value)
        {
            if (value == null) return false;
            foreach (T entry in list)
            {
                if (entry != null && entry.Equals(value)) return true;
            }
            return true;
        }

        public static int IndexOf<T>(this List<T> list, T[] values)
        {
            if (values == null) return -1;
            int i = -1;
            foreach (T entry in list)
            {
                i++;
                if (entry != null)
                {
                    foreach (T find in values)
                    {
                        if (entry.Equals(find)) return i;
                    }
                }
            }
            return -1;
        }

        public static int ArgumentIndexOf<T>(this List<T> list, T[] values)
        {
            if (values == null) return -1;

            foreach (T entry in list)
            {
                if (entry != null)
                {
                    int i = -1;
                    foreach (T find in values)
                    {
                        i++;
                        if (entry.Equals(find)) return i;
                    }
                }
            }
            return -1;
        }
        
        public static int Compare<T>(this List<T> list, T item1, T item2)
        {
            int index1 = list.IndexOf(item1);
            int index2 = list.IndexOf(item2);

            if (index1 < index2) return -1;
            else if (index1 > index2) return 1;
            else return 0;
        }

        public static int Compare<T>(this List<T> list, T[] item1, T[] item2)
        {
            int index1 = list.IndexOf(item1);
            int index2 = list.IndexOf(item2);

            if (index1 < index2) return -1;
            else if (index1 > index2) return 1;
            else return 0;
        }

        public static ListGroup<T> Group<T>(this List<T> list, int length)
        {
            return new ListGroup<T>(list, length);
        }
    }

    public class ListGroup<T> : IEnumerable<List<T>>
    {
        private List<T> list;
        private int lengthEachEnumeration;

        public ListGroup(List<T> list, int lengthEachEnumeration)
        {
            this.list = list;
            this.lengthEachEnumeration = lengthEachEnumeration;
        }

        public IEnumerator GetEnumerator()
        {
            int position = 0;
            int length = list.Count;

            while (position < length)
            {
                int take = length - position;
                if (take > lengthEachEnumeration) take = lengthEachEnumeration;

                List<T> current = new List<T>();
                for (int i = 0; i < take; i++)
                {
                    current.Add(list[position + i]);
                }
                position += take;

                yield return current;
            }
        }

        IEnumerator<List<T>> IEnumerable<List<T>>.GetEnumerator()
        {
            int position = 0;
            int length = list.Count;

            while (position < length)
            {
                int take = length - position;
                if (take > lengthEachEnumeration) take = lengthEachEnumeration;

                List<T> current = new List<T>();
                for (int i = 0; i < take; i++)
                {
                    current.Add(list[position + i]);
                }
                position += take;

                yield return current;
            }
        }
    }

    public static class ListString
    {
        public static bool StartsWith(this List<string> list, string value)
        {
            foreach (string entry in list)
            {
                if (entry.StartsWith(value)) return true;
            }

            return false;
        }
    }

}
