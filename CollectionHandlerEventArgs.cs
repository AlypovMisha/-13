using System;

namespace Лабораторная_13
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string ChangeType { get; set; }
        public object ChangedItem { get; set; }
        public string CollectionName { get; set; }

        public CollectionHandlerEventArgs(string changeType, object changedItem, string collectionName)
        {
            ChangeType = changeType;
            ChangedItem = changedItem;
            CollectionName = collectionName;
        }

        public override string ToString()
        {
            return $"{CollectionName} - Тип изменения: {ChangeType}, Измененный элемент: {ChangedItem}";
        }
    }
}

