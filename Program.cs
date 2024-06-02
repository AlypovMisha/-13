using System;

namespace Лабораторная_13
{
    class Program
    {
        static void Main(string[] args)
        {
            MyObservableCollection<Cars> collection1 = new MyObservableCollection<Cars>("Коллекция 1");
            MyObservableCollection<Cars> collection2 = new MyObservableCollection<Cars>("Коллекция 2");

            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            collection1.CollectionCountChanged += journal1.AddEntry;
            collection1.CollectionReferenceChanged += journal1.AddEntry;

            collection2.CollectionCountChanged += journal2.AddEntry;
            collection2.CollectionReferenceChanged += journal2.AddEntry;

            // Добавление элементов
            Cars car1 = new Cars("Toyota", 2020, "Red", 100000, 15, 1);
            Cars car2 = new Cars("Honda", 2019, "Blue", 90000, 14, 2);
            collection1.Add(car1);
            collection1.Add(car2);

            // Удаление элемента
            collection1.Remove(car1);

            // Замена элемента
            Cars car3 = new Cars("Ford", 2018, "Green", 85000, 16, 3);
            collection1[0] = car3;

            // Добавление в другую коллекцию
            collection2.Add(car3);

            // Вывод журнала
            Console.WriteLine("Journal 1:");
            journal1.PrintJournal();

            Console.WriteLine("Journal 2:");
            journal2.PrintJournal();

           Console.ReadLine();
        }
    }
}


