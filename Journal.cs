using System;
using System.Collections.Generic;

namespace Лабораторная_13
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ChangedItem { get; set; }

        public JournalEntry(string collectionName, string changeType, string changedItem)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedItem = changedItem;
        }

        public override string ToString()
        {
            return $"{CollectionName} - тип изменения: {ChangeType}, измененный элемент: {ChangedItem}";
        }
    }

    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();

        public void AddEntry(object source, CollectionHandlerEventArgs args)
        {
            entries.Add(new JournalEntry(args.CollectionName, args.ChangeType, args.ChangedItem.ToString()));
        }

        public void PrintJournal()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}

