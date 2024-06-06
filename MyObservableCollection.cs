using System;
using System.Collections.Generic;

namespace Лабораторная_13
{
    public class MyObservableCollection<T> : SuperHashTable<T> where T : IInit, ICloneable, new()
    {
        public string CollectionName { get; set; }
        public event EventHandler<CollectionHandlerEventArgs> CollectionCountChanged;
        public event EventHandler<CollectionHandlerEventArgs> CollectionReferenceChanged;

        public MyObservableCollection(string name)
        {
            CollectionName = name;
        }

        public new void Add(T item)
        {
            base.Add(item);
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

        public T this[int index]
        {
            get => GetElementByIndex(index);
            set
            {
                T oldItem = GetElementByIndex(index);
                base.Remove(oldItem);
                base.Add(value);
                OnCollectionReferenceChanged(new CollectionHandlerEventArgs("Заменен элемент", value, CollectionName));
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

        private T GetElementByIndex(int index)
        {
            int count = 0;
            foreach (var item in this)
            {
                if (count == index)
                    return item;
                count++;
            }
            throw new IndexOutOfRangeException();
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
                var node = SearchItem(key);
                if (node != null)
                {
                    base.Remove(node.Data);
                }
                base.Add(value);
                OnCollectionReferenceChanged(new CollectionHandlerEventArgs("Заменен элемент", value, CollectionName));
            }
        }
    }
}



