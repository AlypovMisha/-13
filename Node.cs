using _10LabDll;
using System;

namespace _12LabLibrary
{
    public class Node<T> where T : IInit, ICloneable, new()
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node()
        {
            Data = default(T);
            Next = null;
            Prev = null;
        }

        public Node(T data)
        {
            Data = (T)data.Clone();
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        public Node<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Node<T>(data);
        }

        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }
    }
}
