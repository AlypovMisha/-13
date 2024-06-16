using System;
namespace _10LabDll
{
    public class OffRoadCar : TruckCar
    {
        //Тип бездорожья
        private string offRoadType;

        //Массив с типами бездорожья
        static String[] offRoadTypes = { "gravel roads", "sand dunes", "swamps and bogs", "mountain trails and passes", "forest roads", "desert plains", "snow and ice surfaces", "concrete or stone tracks" };

        //Конструктор без параметров
        public OffRoadCar() : base()
        {
            Awd = false;
            offRoadType = "gravel roads";
        }

        //Конструктор с параметрами
        public OffRoadCar(string brand, int releaseYear, string color, int cost, double clearance, int loadCapacity, bool awd, string offRoadType, int idNumber) : base(brand, releaseYear, color, cost, clearance, loadCapacity, idNumber)
        {
            Awd = awd;
            OffRoadType = offRoadType;
        }

        // Свойство для доступа к полному приводу
        public bool Awd { get; set; }

        // Свойство для доступа к типу бездорожья
        public string OffRoadType
        {
            get
            {
                return offRoadType;
            }
            set
            {
                bool isInArray = Array.Exists(offRoadTypes, element => element == value);
                if (isInArray)
                    offRoadType = value;
                else
                    offRoadType = "gravel roads";
            }
        }

        //Метод для вывода информации о машине
        public override void Show()
        {
            base.Show();
            Console.WriteLine("Присутствие полного привода: " + Awd);
            Console.WriteLine("Тип бездорожья: " + offRoadType);
        }
        //Метод для вывода информации о машине не виртуальный
        public new void ShowNotVirtual()
        {
            base.ShowNotVirtual();
            Console.WriteLine("Присутствие полного привода: " + Awd);
            Console.WriteLine("Тип бездорожья: " + offRoadType);
        }


        //Метод для ввода информации об автомобиле
        public override void Init()
        {
            base.Init();
            Console.Write("Введите привод (true - полный, false - не полный): ");
            bool awFlag;
            if (!bool.TryParse(Console.ReadLine(), out awFlag))
            {
                Console.WriteLine("Некорректный ввод. Привод будет установлен в значение по умолчанию.");
                awFlag = false;
            }
            Awd = awFlag;
            Console.Write("Введите тип бездорожья: ");
            OffRoadType = Console.ReadLine();
        }

        //Метод для ввода информации об автомобиле случайно
        public override void RandomInit()
        {
            base.RandomInit();
            Awd = rand.Next(0, 2) == 1;
            OffRoadType = offRoadTypes[rand.Next(offRoadTypes.Length)];
        }

        //Метод для сравнения
        public override bool Equals(object obj)
        {
            if (obj is OffRoadCar car)
                return (Awd == car.Awd) && (OffRoadType == car.OffRoadType) && base.Equals(car);
            else
                return false;
        }

        //Реализация интерфейса ICloneable
        public override object Clone()
        {
            return new OffRoadCar(Brand, ReleaseYear, Color, Cost, Clearance, loadCapacity, Awd, OffRoadType, Id);
        }


        public override string ToString()
        {
            return $"{Brand}, {ReleaseYear}, {Color}, {Cost}, {Clearance}, {Awd}, {OffRoadType}, {LoadCapacity}, {Id}";
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Brand.GetHashCode();
                hash = hash * 23 + ReleaseYear.GetHashCode();
                hash = hash * 23 + Color.GetHashCode();
                hash = hash * 23 + Cost.GetHashCode();
                hash = hash * 23 + Clearance.GetHashCode();
                hash = hash * 23 + LoadCapacity.GetHashCode();
                hash = hash * 23 + Awd.GetHashCode();

                return hash;

            }
        }
    }
}