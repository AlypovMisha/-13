using _10LabDll;
using System;
using System.Collections.ObjectModel;
namespace Лабораторная_13
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintMenuForObservableCollection();
            ChoiceForObservableCollection();
        }
        public static void PrintMenuForObservableCollection()
        {
            Console.WriteLine("\n\t\t||| Меню |||\n");
            Console.WriteLine("" +
                "1. Добавить элемент в первую коллекцию \n" +
                "2. Удалить элемент из первой коллекции\n" +
                "3. Заменить элемент в первой коллекции\n" +
                "4. Показать элементы первой коллекции\n" +
                "5. Показать журнал №1\n" +
                "6. Добавить элемент во вторую коллекцию\n" +
                "7. Удалить элемент из второй коллекции\n" +
                "8. Заменить элемент во второй коллекции\n" +
                "9. Показать элементы второй коллекции\n" +
                "10. Показать журнал №2\n" +
                "11. Вывести меню\n" + 
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
                                collection1 = new MyObservableCollection<Cars>(name, 10);
                                collection1.CollectionCountChanged += journal1.AddEntry;
                                collection1.CollectionReferenceChanged += journal1.AddEntry;
                                collection1.CollectionReferenceChanged += journal2.AddEntry;
                            }
                            Console.WriteLine("Введите элемент, который хотите добавить");
                            int count = collection1.Count;
                            Cars car = new Cars();
                            car.Init();
                            collection1.Add(car);
                            if (count < collection1.Count)
                                Console.WriteLine("Машина добавлена.");
                            else
                                Console.WriteLine("Машина не добавлена");
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
                                Console.Write("Элемент который хотите удалить: ");                                
                                Cars car = new Cars();
                                car.Init();
                                if(collection1.Remove(car))
                                    Console.WriteLine("Элемент удален из коллекции 1.");
                                else
                                    Console.WriteLine("Элемент не найден");
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
                                Console.WriteLine("Введите элемент, который хотите заменить");
                                Cars oldCar = new Cars();
                                oldCar.Init();
                                Console.WriteLine("Введите элемент, на который хотите заменить");
                                Cars newCar = new Cars();
                                newCar.Init();
                                collection1[oldCar] = newCar;                                
                            }
                            else
                            {
                                Console.WriteLine("В коллекции 1 нет элементов или она еще не создана.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;

                    case "4":
                        if (collection1 != null)
                        {
                            Console.WriteLine("\nЭлементы коллекции 1:");
                            collection1.PrintTable();
                        }
                        else
                        {
                            Console.WriteLine("В коллекции 1 нет элементов или она еще не создана.");
                        }
                        break;

                    case "5":
                        if (journal1.entries.Count > 0)
                        {
                            Console.WriteLine("\nЖурнал №1:");
                            journal1.PrintJournal();
                        }
                        else
                        {
                            Console.WriteLine("Журнал №1 пуст.");
                        }
                        break;

                    case "6":
                        try
                        {
                            if (collection2 == null)
                            {
                                Console.Write("Введите название коллекции: ");
                                string name = Console.ReadLine();
                                collection2 = new MyObservableCollection<Cars>(name, 10);    
                                collection2.CollectionReferenceChanged += journal2.AddEntry;
                            }
                            int count = collection2.Count;
                            Cars car = new Cars();
                            car.Init();
                            collection2.Add(car);
                            if (count < collection2.Count)
                                Console.WriteLine("Машина добавлена.");
                            else
                                Console.WriteLine("Машина не добавлена");
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
                                Console.Write("Элемент который хотите удалить: ");
                                Cars car = new Cars();
                                car.Init();
                                if (collection2.Remove(car))
                                    Console.WriteLine("Элемент удален из коллекции 2.");
                                else
                                    Console.WriteLine("Элемент не найден");
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
                                Console.WriteLine("Введите элемент, который хотите заменить");
                                Cars oldCar = new Cars();
                                oldCar.Init();
                                Console.WriteLine("Введите элемент, на который хотите заменить");
                                Cars newCar = new Cars();
                                newCar.Init();
                                collection2[oldCar] = newCar;

                            }
                            else
                            {
                                Console.WriteLine("В коллекции 2 нет элементов или она еще не создана.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                        break;
                    case "9":
                        if (collection2 != null)
                        {
                            Console.WriteLine("\nЭлементы коллекции 2:");
                            collection2.PrintTable();
                        }
                        else
                        {
                            Console.WriteLine("В коллекции 2 нет элементов или она еще не создана.");
                        }
                        break;

                    case "10":
                        if (journal2.entries.Count > 0)
                        {
                            Console.WriteLine("\nЖурнал №2:");
                            journal2.PrintJournal();
                        }
                        else
                        {
                            Console.WriteLine("Журнал №2 пуст.");
                        }
                        break;
                    case "11":
                        PrintMenuForObservableCollection();
                       break;
                    case "0":
                        break;
                    default:
                        {
                            Console.WriteLine("Вы ввели неправильное значение введите ещё раз");
                            break;
                        }
                }
            }
        }
    }
}

