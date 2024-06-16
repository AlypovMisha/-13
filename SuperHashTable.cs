using System;
using System.Collections;
using System.Collections.Generic;
using _10LabDll;

namespace _12LabLibrary;

public class SuperHashTable<T> : HashTable<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : IInit, ICloneable, new()
{
    public bool IsReadOnly => false;

    public SuperHashTable()
    {
    }

    public SuperHashTable(int size)
        : base(size)
    {
    }

    public SuperHashTable(SuperHashTable<T> c)
        : base(c.table.Count)
    {
        foreach (Node<T> item in c.table)
        {
            if (item != null)
            {
                Add((T)item.Data.Clone());
                for (Node<T> next = item.Next; next != null; next = next.Next)
                {
                    Add((T)next.Data.Clone());
                }
            }
        }
    }

    public new void Add(T data)
    {
        base.Add(data);
    }

    public bool Remove(T data)
    {
        return RemoveElement(data);
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
        foreach (Node<T> node in table)
        {
            for (Node<T> current = node; current != null; current = current.Next)
            {
                yield return current.Data;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Clear()
    {
        base.Count = 0;
        table = new List<Node<T>>(new Node<T>[table.Count]);
    }

    public void DeleteSuperHashTable()
    {
        Clear();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException("array");
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException("arrayIndex");
        }

        if (array.Length - arrayIndex < base.Count)
        {
            throw new ArgumentException("Недостаточно места в целевом массиве.");
        }

        int num = arrayIndex;
        foreach (Node<T> item in table)
        {
            for (Node<T> node = item; node != null; node = node.Next)
            {
                array[num++] = node.Data;
            }
        }
    }

    public SuperHashTable<T> MakeSurfaceCopy()
    {
        SuperHashTable<T> superHashTable = new SuperHashTable<T>(table.Count);
        for (int i = 0; i < table.Count; i++)
        {
            superHashTable.table[i] = table[i];
            if (superHashTable.table[i] != null)
            {
                superHashTable.Count++;
            }
        }

        return superHashTable;
    }
}
