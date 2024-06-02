using System;

namespace Лабораторная_13
{
    public interface IInit
    {
        void Init();
        void RandomInit();
        void Show();
    }

    public class Cars : IInit, IComparable, ICloneable
    {
        //Марка автомобиля
        protected string brand;
        //Год выпуска
        protected int releaseYear;
        //Цвет
        protected string color;
        //Цена
        protected int cost;
        //Дорожный просвет
        protected double clearance;
        //id
        protected int id;

        //Массивы для заполнения информации об автомобиле
        static string[] colors = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Pink", "Brown", "Black", "White", "Gray" };
        static string[] brands = { "Toyota", "Honda", "BMW", "Mercedes", "Ford", "Chevrolet", "Volkswagen", "Audi", "Nissan", "Hyundai" };

        protected static Random rand = new Random();

        //Конструктор без параметра
        public Cars()
        {
            Brand = "Toyota";
            ReleaseYear = 2020;
            Color = "White";
            Cost = 100000;
            Clearance = 15;
        }

        //Конструктор с параметрами
        public Cars(string brand, int releaseYear, string color, int cost, double clearance, int idNumber)
        {
            Brand = brand;
            ReleaseYear = releaseYear;
            Color = color;
            Cost = cost;
            Clearance = clearance;
            Id = idNumber;
        }

        //Конструктор копирования
        public Cars(Cars car)
        {
            Brand = car.Brand;
            ReleaseYear = car.ReleaseYear;
            Color = car.Color;
            Cost = car.Cost;
            Clearance = car.Clearance;
            Id = car.Id;
        }

        public Cars(string brand, int releaseYear, string color, int cost, double clearance)
        {
            Brand = brand;
            ReleaseYear = releaseYear;
            Color = color;
            Cost = cost;
            Clearance = clearance;
        }

        //Свойство доступа к id
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value > 0) { id = value; }
            }
        }

        // Свойство для доступа к марке
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                bool isInArray = Array.Exists(brands, element => element == value);
                if (isInArray)
                    brand = value;
                else
                {
                    string name = "Toyota";
                    while (!isInArray)
                    {
                        Console.Write("Вы неправильно ввели название, введите ещё раз: ");
                        name = Console.ReadLine();
                        isInArray = Array.Exists(brands, element => element == name);
                    }
                    brand = name;
                }
            }
        }

        // Свойство для доступа к году выпуска
        public int ReleaseYear
        {
            get
            {
                return releaseYear;
            }
            set
            {
                if (value > 2024)
                    releaseYear = 2024;
                else if (value < 1980)
                    releaseYear = 1980;
                else
                    releaseYear = value;
            }
        }

        // Свойство для доступа к цвету
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                bool isInArray = Array.Exists(colors, element => element == value);
                if (isInArray)
                    color = value;
                else
                {
                    string clr = "White";
                    while (!isInArray)
                    {
                        Console.Write("Вы неправильно ввели цвет, введите ещё раз: ");
                        clr = Console.ReadLine();
                        isInArray = Array.Exists(colors, element => element == clr);
                    }
                    color = clr;
                }
            }
        }

        // Свойство для доступа к цене
        public int Cost
        {
            get
            {
                return cost;
            }
            set
            {
                if (value > 100000000)
                    cost = 100000000;
                else if (value < 10000)
                    cost = 10000;
                else
                    cost = value;
            }
        }

        // Свойство для доступа к клиренсу
        public double Clearance
        {
            get
            {
                return clearance;
            }
            set
            {
                if (value > 40)
                    clearance = 40;
                else if (value < 10)
                    clearance = 10;
                else
                    clearance = value;
            }
        }

        //Метод для вывода информации о машине
        public virtual void Show()
        {
            Console.WriteLine($"Марка автомобиля: \"{Brand}\"");
            Console.WriteLine($"Год выпуска: {ReleaseYear}");
            Console.WriteLine($"Цвет: {Color}");
            Console.WriteLine($"Цена (в руб): {Cost}");
            Console.WriteLine($"Клиренс (в см): {Clearance}");
            Console.WriteLine($"id: {id}");
        }

        //Метод для вывода информации о машине не виртуальный
        public void ShowNotVirtual()
        {
            Console.WriteLine($"Марка автомобиля: \"{Brand}\"");
            Console.WriteLine($"Год выпуска: {ReleaseYear}");
            Console.WriteLine($"Цвет: {Color}");
            Console.WriteLine($"Цена (в руб): {Cost}");
            Console.WriteLine($"Клиренс (в см): {Clearance}");
        }

        //Метод для ввода информации об автомобиле
        public virtual void Init()
        {
            Console.Write("Введите марку автомобиля: ");
            Brand = Console.ReadLine();
            Console.Write("Введите год выпуска автомобиля: ");
            int vl = ParseToInt(Console.ReadLine());
            ReleaseYear = vl;
            Console.Write("Введите цвет автомобиля: ");
            Color = Console.ReadLine();
            Console.Write("Введите цену автомобиля: ");
            vl = ParseToInt(Console.ReadLine());
            Cost = vl;
            Console.Write("Введите клиренс автомобиля: ");
            vl = ParseToInt(Console.ReadLine());
            Clearance = vl;
            Console.Write("Введите id автомобиля: ");
            vl = ParseToInt(Console.ReadLine());
            Id = vl;
        }

        //Метод для ввода информации об автомобиле случайно
        public virtual void RandomInit()
        {
            Brand = brands[rand.Next(brands.Length)];
            ReleaseYear = rand.Next(1980, 2024);
            Color = colors[rand.Next(colors.Length)];
            Cost = rand.Next(10000, 1000000);
            Clearance = rand.Next(10, 40);
            Id = rand.Next(1, 1001);
        }

        //Метод для сравнения
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Cars car)
            {
                return Brand == car.Brand
                    && ReleaseYear == car.ReleaseYear
                    && Clearance == car.Clearance
                    && Cost == car.Cost
                    && Color == car.Color
                    && Id == car.Id;
            }
            return false;
        }


        //Для преобразования в int
        protected int ParseToInt(string str)
        {
            int vl;
            bool success = int.TryParse(str, out vl);
            if (success)
                return vl;
            while (!success)
            {
                Console.Write("Вы неправильно ввели значение, введите ещё раз: ");
                success = int.TryParse(Console.ReadLine(), out vl);
            }
            return vl;
        }

        // Реализация интерфейса Comparable
        public virtual int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Cars car = obj as Cars;
            if (car != null)
            {
                //сравниваем по цене
                int costComparison = Cost.CompareTo(car.Cost);
                if (costComparison != 0)
                {
                    return costComparison;
                }

                //сравниваем по марке автомобиля
                int brandComparison = Brand.CompareTo(car.Brand);
                if (brandComparison != 0)
                {
                    return brandComparison;
                }

                //сравниваем по году выпуска
                int yearComparison = ReleaseYear.CompareTo(car.ReleaseYear);
                if (yearComparison != 0)
                {
                    return yearComparison;
                }

                //сравниваем по клиренсу
                int clearanceComparison = Clearance.CompareTo(car.Clearance);
                if (clearanceComparison != 0)
                {
                    return clearanceComparison;
                }
                int colorComparison = Color.CompareTo(car.Color);
                if (colorComparison != 0)
                {
                    return colorComparison;
                }

                return GetType().Name.CompareTo(obj.GetType().Name);
            }
            else
            {
                throw new ArgumentException("Объект не является экземпляром класса Cars.");
            }
        }

        //Реализация интерфейса ICloneable
        public virtual object Clone()
        {
            return new Cars(this);
        }

        public override string ToString()
        {
            return $"{Brand}, {ReleaseYear}, {Color}, {Cost}, {Clearance}, {Id}";
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Brand.GetHashCode();
                hash = hash * 3 + ReleaseYear.GetHashCode();
                hash = hash * 7 + Color.GetHashCode();
                hash = hash * 11 + Cost.GetHashCode();
                hash = hash * 13 + Clearance.GetHashCode();
                hash = hash * 6 + Id.GetHashCode();
                return hash;
            }
        }
    }
}
