using _10LabDll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Лабораторная_13;

namespace UnitTestProject5
{
    [TestClass]
    public class MyObservableCollectionTests
    {
        private MyObservableCollection<Cars> collection;

        [TestInitialize]
        public void SetUp()
        {
            collection = new MyObservableCollection<Cars>("CarsCollection", 10);
        }

        [TestMethod]
        public void Indexer_Get_ExistingElement_ReturnsElement()
        {
            var collection = new MyObservableCollection<Cars>("CarsCollection", 10);
            var car = new Cars("Toyota", 2022, "White", 20000, 15.0, 1);
            collection.Add(car);

            var retrievedCar = collection[car];

            Assert.AreEqual(car, retrievedCar);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Indexer_Get_NonExistingElement_ThrowsException()
        {
            var collection = new MyObservableCollection<Cars>("CarsCollection", 10);
            var car = new Cars("Toyota", 2022, "White", 20000, 15.0, 1);

            var retrievedCar = collection[car];
        }

        [TestMethod]
        public void Indexer_Set_ExistingElement_ReplacesElement()
        {
            var collection = new MyObservableCollection<Cars>("CarsCollection", 10);
            var car1 = new Cars("Toyota", 2022, "White", 20000, 15.0, 1);
            var car2 = new Cars("Honda", 2021, "Black", 25000, 14.0, 2);
            collection.Add(car1);

            collection[car1] = car2;

            var retrievedCar = collection[car2];
            Assert.AreEqual(car2, retrievedCar);
            Assert.IsFalse(collection.Contains(car1));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Ключа не было в коллекции")]
        public void Indexer_Set_NonExistingElement_ThrowsException()
        {
            var collection = new MyObservableCollection<Cars>("CarsCollection", 10);
            var car1 = new Cars("Toyota", 2022, "White", 20000, 15.0, 1);
            var car2 = new Cars("Honda", 2021, "Black", 25000, 14.0, 2);

            collection[car1] = car2;
        }

        [TestMethod]
        public void Indexer_Set_ElementAlreadyInCollection_WritesMessage()
        {
            var collection = new MyObservableCollection<Cars>("CarsCollection", 10);
            var car1 = new Cars("Toyota", 2022, "White", 20000, 15.0, 1);
            var car2 = new Cars("Honda", 2021, "Black", 25000, 14.0, 2);
            collection.Add(car1);
            collection.Add(car2);

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                collection[car1] = car2;

                var expected = "Элемент уже есть в коллекции" + Environment.NewLine;
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }

    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string expectedCollectionName = "TestCollection";
            string expectedChangeType = "Added";
            string expectedChangedItem = "TestItem";

            // Act
            JournalEntry entry = new JournalEntry(expectedCollectionName, expectedChangeType, expectedChangedItem);

            // Assert
            Assert.AreEqual(expectedCollectionName, entry.CollectionName);
            Assert.AreEqual(expectedChangeType, entry.ChangeType);
            Assert.AreEqual(expectedChangedItem, entry.ChangedItem);
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectString()
        {
            // Arrange
            string collectionName = "TestCollection";
            string changeType = "Added";
            string changedItem = "TestItem";
            string expectedString = $"Название коллекции: {collectionName}. Тип изменения: {changeType}.  Элемент: {changedItem}";

            JournalEntry entry = new JournalEntry(collectionName, changeType, changedItem);

            // Act
            string result = entry.ToString();

            // Assert
            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        public void CollectionNameProperty_ShouldGetAndSetCorrectly()
        {
            // Arrange
            JournalEntry entry = new JournalEntry("InitialCollection", "Added", "TestItem");
            string newCollectionName = "NewCollection";

            // Act
            entry.CollectionName = newCollectionName;

            // Assert
            Assert.AreEqual(newCollectionName, entry.CollectionName);
        }

        [TestMethod]
        public void ChangeTypeProperty_ShouldGetAndSetCorrectly()
        {
            // Arrange
            JournalEntry entry = new JournalEntry("TestCollection", "InitialChange", "TestItem");
            string newChangeType = "Updated";

            // Act
            entry.ChangeType = newChangeType;

            // Assert
            Assert.AreEqual(newChangeType, entry.ChangeType);
        }

        [TestMethod]
        public void ChangedItemProperty_ShouldGetAndSetCorrectly()
        {
            // Arrange
            JournalEntry entry = new JournalEntry("TestCollection", "Added", "InitialItem");
            string newChangedItem = "NewItem";

            // Act
            entry.ChangedItem = newChangedItem;

            // Assert
            Assert.AreEqual(newChangedItem, entry.ChangedItem);
        }


    }
    [TestClass]
    public class JournalTests
    {
        [TestMethod]
        public void AddEntry_ShouldAddNewEntry()
        {
            // Arrange
            var journal = new Journal();
            var args = new CollectionHandlerEventArgs("Added", "TestItem", "TestCollection");

            // Act
            journal.AddEntry(this, args);

            // Assert
            Assert.AreEqual(1, journal.entries.Count);
            Assert.AreEqual("TestCollection", journal.entries[0].CollectionName);
            Assert.AreEqual("Added", journal.entries[0].ChangeType);
            Assert.AreEqual("TestItem", journal.entries[0].ChangedItem);
        }

        [TestMethod]
        public void PrintJournal_ShouldPrintEntries()
        {
            // Arrange
            var journal = new Journal();
            var args = new CollectionHandlerEventArgs("Added", "TestItem", "TestCollection");
            journal.AddEntry(this, args);
            args = new CollectionHandlerEventArgs("Removed", "AnotherItem", "AnotherCollection");
            journal.AddEntry(this, args);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                journal.PrintJournal();

                // Assert
                var expectedOutput = $"Название коллекции: TestCollection. Тип изменения: Added.  Элемент: TestItem{Environment.NewLine}" +
                                     $"Название коллекции: AnotherCollection. Тип изменения: Removed.  Элемент: AnotherItem{Environment.NewLine}";
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        [TestMethod]
        public void PrintJournal_ShouldPrintNoEntriesMessage()
        {
            // Arrange
            var journal = new Journal();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                journal.PrintJournal();

                // Assert
                var expectedOutput = "В журнале нет записей" + Environment.NewLine;
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }

        [TestMethod]
        public void Entries_ShouldBeEmptyInitially()
        {
            // Arrange
            var journal = new Journal();

            // Act & Assert
            Assert.AreEqual(0, journal.entries.Count);
        }

        [TestMethod]
        public void AddEntry_ShouldHandleNullSource()
        {
            // Arrange
            var journal = new Journal();
            var args = new CollectionHandlerEventArgs("Added", "TestItem", "TestCollection");

            // Act
            journal.AddEntry(null, args);

            // Assert
            Assert.AreEqual(1, journal.entries.Count);
            Assert.AreEqual("TestCollection", journal.entries[0].CollectionName);
            Assert.AreEqual("Added", journal.entries[0].ChangeType);
            Assert.AreEqual("TestItem", journal.entries[0].ChangedItem);
        }
    }

    [TestClass]
    public class CollectionHandlerEventArgsTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string changeType = "Added";
            object changedItem = "TestItem";
            string collectionName = "TestCollection";

            // Act
            var args = new CollectionHandlerEventArgs(changeType, changedItem, collectionName);

            // Assert
            Assert.AreEqual(changeType, args.ChangeType);
            Assert.AreEqual(changedItem, args.ChangedItem);
            Assert.AreEqual(collectionName, args.CollectionName);
        }

        [TestMethod]
        public void ChangeType_Property_ShouldBeSettable()
        {
            // Arrange
            var args = new CollectionHandlerEventArgs("Added", "TestItem", "TestCollection");

            // Act
            args.ChangeType = "Removed";

            // Assert
            Assert.AreEqual("Removed", args.ChangeType);
        }

        [TestMethod]
        public void ChangedItem_Property_ShouldBeSettable()
        {
            // Arrange
            var args = new CollectionHandlerEventArgs("Added", "TestItem", "TestCollection");
            var newItem = new object();

            // Act
            args.ChangedItem = newItem;

            // Assert
            Assert.AreEqual(newItem, args.ChangedItem);
        }

        [TestMethod]
        public void CollectionName_Property_ShouldBeSettable()
        {
            // Arrange
            var args = new CollectionHandlerEventArgs("Added", "TestItem", "TestCollection");

            // Act
            args.CollectionName = "NewCollection";

            // Assert
            Assert.AreEqual("NewCollection", args.CollectionName);
        }


        [TestClass]
        public class MyObservableCollectionTests
        {
            [TestMethod]
            public void Add_ShouldRaiseCollectionCountChangedEvent()
            {
                // Arrange
                var collection = new MyObservableCollection<Cars>("TestCarsCollection", 10);
                var car = new Cars("Toyota", 2020, "White", 30000, 15, 1);
                bool eventRaised = false;
                collection.CollectionCountChanged += (sender, args) => { eventRaised = true; };

                // Act
                collection.Add(car);

                // Assert
                Assert.IsTrue(eventRaised);
                Assert.AreEqual(1, collection.Count);
            }

            [TestMethod]
            public void Remove_ShouldRaiseCollectionCountChangedEvent()
            {
                // Arrange
                var collection = new MyObservableCollection<Cars>("TestCarsCollection", 10);
                var car = new Cars("Toyota", 2020, "White", 30000, 15, 1);
                collection.Add(car);
                bool eventRaised = false;
                collection.CollectionCountChanged += (sender, args) => { eventRaised = true; };

                // Act
                collection.Remove(car);

                // Assert
                Assert.IsTrue(eventRaised);
                Assert.AreEqual(0, collection.Count);
            }

            [TestMethod]
            public void ReplaceElement_ShouldReplaceAndRaiseCollectionReferenceChangedEvent()
            {
                // Arrange
                var collection = new MyObservableCollection<Cars>("TestCarsCollection", 10);
                var oldCar = new Cars("Toyota", 2020, "White", 30000, 15, 1);
                var newCar = new Cars("Honda", 2022, "Red", 40000, 14, 2);
                collection.Add(oldCar);
                bool eventRaised = false;
                collection.CollectionReferenceChanged += (sender, args) => { eventRaised = true; };

                // Act
                collection.ReplaceElement(oldCar, newCar);

                // Assert
                Assert.IsTrue(eventRaised);
                Assert.IsFalse(collection.Contains(oldCar));
                Assert.IsTrue(collection.Contains(newCar));
            }

            [TestMethod]
            public void Constructor_ShouldInitializeCollectionName()
            {
                // Arrange
                string collectionName = "TestCarsCollection";
                int size = 10;

                // Act
                var collection = new MyObservableCollection<Cars>(collectionName, size);

                // Assert
                Assert.AreEqual(collectionName, collection.CollectionName);
            }

            [TestMethod]
            public void ReplaceElement_SuccessfulReplacement_ShouldRaiseCollectionReferenceChangedEvent()
            {
                var collection = new MyObservableCollection<Cars>("TestCarsCollection", 10);
                var oldCar = new Cars("Toyota", 2020, "White", 30000, 15, 1);
                var newCar = new Cars("Honda", 2022, "Red", 40000, 14, 2);
                collection.Add(oldCar);
                bool eventRaised = false;
                collection.CollectionReferenceChanged += (sender, args) => { eventRaised = true; };

                bool result = collection.ReplaceElement(oldCar, newCar);

                Assert.IsTrue(result);
                Assert.IsTrue(eventRaised);
                Assert.IsFalse(collection.Contains(oldCar));
                Assert.IsTrue(collection.Contains(newCar));
            }


            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void ReplaceElement_ItemNotFound_ShouldThrowArgumentException()
            {
                var collection = new MyObservableCollection<Cars>("TestCarsCollection", 10);
                var oldCar = new Cars("Toyota", 2020, "White", 30000, 15, 1);
                var newCar = new Cars("Honda", 2022, "Red", 40000, 14, 2);

                collection.ReplaceElement(oldCar, newCar);
            }
        }
    }
}
