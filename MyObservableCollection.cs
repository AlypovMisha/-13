using _10LabDll;
using _12LabLibrary;
using System;
using System.Collections.Generic;

namespace Лабораторная_13
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
    public class MyObservableCollection<T> : SuperHashTable<T> where T : IInit, ICloneable, new()
    {
        public string CollectionName { get; set; }
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public MyObservableCollection(string name, int size) : base(size)
        {
            CollectionName = name;
        }

        public new void Add(T item)
        {
            int c = Count;
            base.Add(item);
            if(c < Count) 
                OnCollectionCountChanged(new CollectionHandlerEventArgs("Добавлен новый элемент", item, CollectionName));
        }

        public new bool Remove(T item)
        {
            bool removed = base.Remove(item);
            if (removed)
            {
                OnCollectionCountChanged(new CollectionHandlerEventArgs("Удален элемент", item, CollectionName));
            }
            return removed;
        }

        public bool ReplaceElement(T oldItem, T newItem)
        {
            if (Contains(oldItem))
            {
                if (oldItem.Equals(newItem))
                    return false;
                Remove(oldItem);
                Add(newItem);
                OnCollectionReferenceChanged(new CollectionHandlerEventArgs($"Заменен элемент", oldItem, CollectionName));
                return true;
            }
            else
            {
                throw new ArgumentException("Элемент не найден в коллекции.");
            }
        }

        protected virtual void OnCollectionCountChanged(CollectionHandlerEventArgs e)
        {
            CollectionCountChanged?.Invoke(this, e);
        }

        protected virtual void OnCollectionReferenceChanged(CollectionHandlerEventArgs e)
        {
            CollectionReferenceChanged?.Invoke(this, e);
        }

        public T this[T key]
        {
            get
            {
                var node = SearchItem(key);
                if (node == null) throw new KeyNotFoundException("Элемент с таким ключом не найден.");
                return node.Data;
            }
            set
            {
                if (!Contains(value))
                {
                    var node = SearchItem(key);
                    if (node != null)
                    {
                        base.Remove(node.Data);
                        base.Add(value);
                        OnCollectionReferenceChanged(new CollectionHandlerEventArgs("Заменен элемент", node.Data, CollectionName));
                        Console.WriteLine("Произошла замена");
                    }
                    else
                    {
                        throw new Exception("Ключа не было в коллекции");
                    }
                }
                else
                {
                    Console.WriteLine("Элемент уже есть в коллекции");
                }
            }
        }
    }
}




