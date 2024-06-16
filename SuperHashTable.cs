using _10LabDll;
using _12LabLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _12LabLibrary
{
    public class SuperHashTable<T> : HashTable<T>, ICollection<T> where T : IInit, ICloneable, new()
    {
        public bool IsReadOnly => false;

        public SuperHashTable() : base()
        {
        }

        public SuperHashTable(int size) : base(size)
        {
        }

        public SuperHashTable(SuperHashTable<T> c) : base(c.table.Count)
        {
            foreach (var node in c.table)
            {
                if (node != null)
                {
                    Add((T)node.Data.Clone());
                    Node<T> current = node.Next;
                    while (current != null)
                    {
                        Add((T)current.Data.Clone());
                        current = current.Next;
                    }
                }
            }
        }

        //private Node<T> Clone(Node<T> node)
        //{
        //    if (node == null) return null;

        //    Node<T> newNode = new Node<T>((T)node.Data.Clone())
        //    {
        //        Next = Clone(node.Next),
        //        Prev = null
        //    };

        //    if (newNode.Next != null)
        //    {
        //        newNode.Next.Prev = newNode;
        //    }

        //    return newNode;
        //}

        public new void Add(T data)
        {
            base.Add(data);
        }

        public bool Remove(T data)
        {
            return base.RemoveElement(data);
        }

        public new Node<T> SearchItem(T itemForSearch)
        {
            return base.SearchItem(itemForSearch);
        }

        public new bool Contains(T data)
        {
            return base.Contains(data);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in table)
            {
                Node<T> current = node;
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            Count = 0;
            table = new List<Node<T>>(new Node<T>[table.Count]);
        }

        public void DeleteSuperHashTable()
        {
            Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Недостаточно места в целевом массиве.");

            int currentIndex = arrayIndex;
            foreach (var node in table)
            {
                Node<T> current = node;
                while (current != null)
                {
                    array[currentIndex++] = current.Data;
                    current = current.Next;
                }
            }
        }

        public SuperHashTable<T> MakeSurfaceCopy()
        {
            SuperHashTable<T> copy = new SuperHashTable<T>(table.Count);
            for (int i = 0; i < table.Count; i++)
            {
                copy.table[i] = table[i];
                if (copy.table[i] != null)
                    copy.Count++;
            }

            return copy;
        }
    }
}
