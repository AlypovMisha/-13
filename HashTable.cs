using System;
using System.Collections.Generic;

namespace Лабораторная_13
{
    public class HashTable<T> where T : IInit, ICloneable, new()
    {
        protected List<Node<T>> table;

        public int Count { get; protected set; }

        public HashTable()
        {
            table = new List<Node<T>>(new Node<T>[10]);
            Count = 0;
        }

        public HashTable(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Размер таблицы должен быть больше 0");
            }
            table = new List<Node<T>>(new Node<T>[size]);
            Count = 0;
        }

        private int GetIndex(T item)
        {
            return Math.Abs(item.GetHashCode()) % table.Count;
        }

        public void Add(T item)
        {
            int index = GetIndex(item);
            Node<T> newNode = new Node<T>(item);
            if (table[index] == null)
            {
                table[index] = newNode;
            }
            else
            {
                Node<T> current = table[index];
                while (current.Next != null)
                {
                    if (current.Data.Equals(item))
                    {
                        return; // Элемент уже существует, не добавляем
                    }
                    current = current.Next;
                }
                if (current.Data.Equals(item))
                {
                    return; // Элемент уже существует, не добавляем
                }
                current.Next = newNode;
            }
            Count++;
        }

        public bool RemoveElement(T item)
        {
            int index = GetIndex(item);
            Node<T> current = table[index];
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    if (previous == null)
                    {
                        table[index] = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    Count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public Node<T> SearchItem(T item)
        {
            int index = GetIndex(item);
            Node<T> current = table[index];

            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        public bool Contains(T item)
        {
            return SearchItem(item) != null;
        }

        public void PrintTable()
        {
            if (table != null)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (table[i] != null)
                    {
                        Console.WriteLine($"{i}: {table[i]}");
                        if (table[i].Next != null)
                        {
                            Node<T> current = table[i].Next;
                            while (current != null)
                            {
                                Console.WriteLine($"   {current.Data}");
                                current = current.Next;
                            }
                        }
                    }
                    else { Console.WriteLine($"{i}: Нет элемента"); }
                }
            }
            else
            {
                throw new Exception("Таблица удалена");
            }
        }

        public void DeleteHashTable()
        {
            table = null;
            Count = 0;
        }
    }
}

