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

            while (true)
            {
                try
                {
                    PrintMenuForObservableCollection();
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 0) break;

                    switch (choice)
                    {
                        case 1:
                            AddCarToCollection(collection1);
                            break;
                        case 2:
                            RemoveCarFromCollection(collection1);
                            break;
                        case 3:
                            ReplaceCarInCollection(collection1);
                            break;
                        case 4:
                            PrintCollection(collection1);
                            break;
                        case 5:
                            PrintJournal(journal1);
                            break;
                        case 6:
                            AddCarToCollection(collection2);
                            break;
                        case 7:
                            RemoveCarFromCollection(collection2);
                            break;
                        case 8:
                            ReplaceCarInCollection(collection2);
                            break;
                        case 9:
                            PrintCollection(collection2);
                            break;
                        case 10:
                            PrintJournal(journal2);
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
        public static void PrintMenuForObservableCollection()
        {
            Console.WriteLine("\n\t\t||| Меню |||\n");
            Console.WriteLine("" +
                "1. Добавить элемент в коллекцию\n" +
                "2. Удалить элемент из коллекции\n" +
                "3. Заменить элемент в коллекции\n" +
                "4. Показать элементы коллекции\n" +
                "5. Показать журнал изменений коллекции\n" +
                "6. Добавить элемент в другую коллекцию\n" +
                "7. Удалить элемент из другой коллекции\n" +
                "8. Заменить элемент в другой коллекции\n" +
                "9. Показать элементы другой коллекции\n" +
                "10. Показать журнал изменений другой коллекции\n" +
                "0. Выйти");
        }

        public static void ChoiceForObservableCollection()
        {
            string choice = null;
            MyObservableCollection<Cars> collection1 = null;
            MyObservableCollection<Cars> collection2 = null;
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            while (choice != "0")
            {
                Console.Write("Ваш выбор: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            if (collection1 == null)
                            {
                                Console.Write("Введите название коллекции: ");
                                string name = Console.ReadLine();
                                collection1 = new MyObservableCollection<Cars>(name);
                            }
                            Cars car = new Cars();
                            car.Init();
                            collection1.Add(car);
                            Console.WriteLine("Элемент добавлен в коллекцию 1.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            if (collection1 != null && collection1.Count > 0)
                            {
                                Console.Write("Введите индекс элемента для удаления из коллекции 1: ");
                                int index = int.Parse(Console.ReadLine());
                                Cars car = collection1[index];
                                collection1.Remove(car);
                                Console.WriteLine("Элемент удален из коллекции 1.");
                            }
                            else
                            {
                                Console.WriteLine("В коллекции 1 нет элементов или она еще не создана.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "3":
                        try
                        {
                            if (collection1 != null && collection1.Count > 0)
                            {
                                Console.Write("Введите индекс элемента для замены в коллекции 1: ");
                                int index = int.Parse(Console.ReadLine());
                                Cars car = new Cars();
                                car.Init();
                                collection1[index] = car;
                                Console.WriteLine("Элемент заменен в коллекции 1.");
                            }
                            else
                            {
                                Console.WriteLine("В коллекции 1 нет элементов или она еще не создана.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "4":
                        if (collection1 != null)
                        {
                            Console.WriteLine("\nЭлементы коллекции 1:");
                            foreach (var car in collection1)
                            {
                                Console.WriteLine(car);
                            }
                        }
                        else
                        {
                            Console.WriteLine("В коллекции 1 нет элементов или она еще не создана.");
                        }
                        break;

                    case "5":
                        if (journal1.entries.Count > 0)
                        {
                            Console.WriteLine("\nЖурнал изменений коллекции 1:");
                            journal1.PrintJournal();
                        }
                        else
                        {
                            Console.WriteLine("Журнал изменений коллекции 1 пуст.");
                        }
                        break;

                    case "6":
                        try
                        {
                            if (collection2 == null)
                            {
                                Console.Write("Введите название коллекции: ");
                                string name = Console.ReadLine();
                                collection2 = new MyObservableCollection<Cars>(name);
                            }
                            Console.Write("Введите индекс элемента для добавления в коллекцию 2: ");
                            int index = int.Parse(Console.ReadLine());
                            if (index >= 0 && index < collection1.Count)
                            {
                                Cars car = collection1[index];
                                collection2.Add(car);
                                Console.WriteLine("Элемент добавлен в коллекцию 2.");
                            }
                            else
                            {
                                Console.WriteLine("Неверный индекс элемента в коллекции 1.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "7":
                        try
                        {
                            if (collection2 != null && collection2.Count > 0)
                            {
                                Console.Write("Введите индекс элемента для удаления из коллекции 2: ");
                                int index = int.Parse(Console.ReadLine());
                                Cars car = collection2[index];
                                collection2.Remove(car);
                                Console.WriteLine("Элемент удален из коллекции 2.");
                            }
                            else
                            {
                                Console.WriteLine("В коллекции 2 нет элементов или она еще не создана.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "8":
                        try
                        {
                            if (collection2 != null && collection2.Count > 0)
                            {
                                Console.Write("Введите индекс элемента для замены в коллекции 2: ");
                                int index = int.Parse(Console.ReadLine());
                                Cars car = new Cars();
                                car.Init();
                                collection2[index] = car;
                                Console.WriteLine("Элемент заменен в коллекции 2.");
                            }
                            else
                            {
                                Console.WriteLine("В коллекции 2 нет элементов или она еще не создана.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "9":
                        if (collection2 != null)
                        {
                            Console.WriteLine("\nЭлементы коллекции 2:");
                            foreach (var car in collection2)
                            {
                                Console.WriteLine(car);
                            }
                        }
                        else
                        {
                            Console.WriteLine("В коллекции 2 нет элементов или она еще не создана.");
                        }
                        break;

                    case "10":
                        if (journal2.entries.Count > 0)
                        {
                            Console.WriteLine("\nЖурнал изменений коллекции 2:");
                            journal2.PrintJournal();
                        }
                        else
                        {
                            Console.WriteLine("Журнал изменений коллекции 2 пуст.");
                        }
                        break;
                }
            }
        }
                            
      

        static void AddCarToCollection(MyObservableCollection<Cars> collection)
        {
            Cars car = new Cars();
            car.Init();
            collection.Add(car);
            Console.WriteLine("Машина добавлена.");
        }

        static void RemoveCarFromCollection(MyObservableCollection<Cars> collection)
        {
            Console.Write("Введите индекс машины для удаления: ");
            int index = int.Parse(Console.ReadLine());
            Cars car = collection[index];
            collection.Remove(car);
            Console.WriteLine("Машина удалена.");
        }

        static void ReplaceCarInCollection(MyObservableCollection<Cars> collection)
        {
            Console.Write("Введите индекс машины для замены: ");
            int index = int.Parse(Console.ReadLine());
            Cars car = new Cars();
            car.Init();
            collection[index] = car;
            Console.WriteLine("Машина заменена.");
        }

        static void PrintCollection(MyObservableCollection<Cars> collection)
        {
            foreach (var car in collection)
            {
                Console.WriteLine(car);
            }
        }

        static void PrintJournal(Journal journal)
        {
            journal.PrintJournal();
        }

    }
}


