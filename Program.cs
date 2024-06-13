using Laba10;
using System.Threading.Channels;
using System.Linq;
using System;
using Lab12_3;

namespace DELETE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string choose = "";
            while (true)
            {
                MainMenu();
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        StartLib10();
                        break;
                    case "2":
                        StartLib12();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ошибка ввода!");
                        break;
                }
            }
        }

        static void StartLib10()
        {
            // Создаем города
            var cities = new List<City>();
            City firstCity = new City("Москва");
            City secondCity = new City("Пермь");
            City thirdCity = new City("Санкт-Петербург");
            
            // Создаем аэропорты и добавляем их в города
            var airports = new List<Airport>();
            Airport firstAirport = new Airport("VKO");
            Airport secondAirport = new Airport("SVO");
            Airport thirdAirport = new Airport("PEE");
            Airport fourthAirport = new Airport("LED");

            // Заполняем аэропорты самолетами
            List<Aircraft> list = GenerateRandomAircrafts(4);
            foreach (var aircraft in list)
            {
                firstAirport.AddAircraft(aircraft);
            }

            list = GenerateRandomAircrafts(5);
            foreach (var aircraft in list)
            {
                secondAirport.AddAircraft(aircraft);
            }

            list = GenerateRandomAircrafts(3);
            foreach (var aircraft in list)
            {
                thirdAirport.AddAircraft(aircraft);
            }

            list = GenerateRandomAircrafts(6);
            foreach (var aircraft in list)
            {
                fourthAirport.AddAircraft(aircraft);
            }

            firstCity.AddAirport(firstAirport);
            firstCity.AddAirport(secondAirport);
            secondCity.AddAirport(thirdAirport);
            thirdCity.AddAirport(fourthAirport);

            string choose = "";
            while (true)
            {
                TenLibMenu();
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        YearHigherThan1970(firstCity);
                        break;
                    case "2":
                        Union(firstCity, secondCity);
                        break;
                    case "3":
                        Min(firstCity);
                        break;
                    case "4":
                        GroupBy(firstCity);
                        break;
                    case "5":
                        JoinAirportsAndAircrafts(new List<City> { firstCity, secondCity });
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ошибка ввода!");
                        break;
                }
            }
        }

        static void StartLib12()
        {
            string choose = "";

            // Создаем города
            City firstCity = new City("Москва");
            City secondCity = new City("Пермь");

            // Создаем аэропорты и добавляем их в города
            Airport firstAirport = new Airport("VKO");
            Airport secondAirport = new Airport("SVO");
            Airport thirdAirport = new Airport("PEE");

            // Создание элементов из 12 лабораторной
            var aircrafts = new MyTree<Aircraft>();
            for (int i = 0; i < 10; i++)
            {
                Aircraft aircraft = new Aircraft();
                aircraft.RandomInit();
                aircrafts.Add(aircraft);
            }

            // Добавление поездов на станции
            firstCity.AddAirport(firstAirport);
            firstCity.AddAirport(secondAirport);
            secondCity.AddAirport(thirdAirport);

            while (true)
            {
                TwelveLibMenu();
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        WhereTree(aircrafts);
                        break;
                    case "2":
                        ConutTree(aircrafts);
                        break;
                    case "3":
                        MinTree(aircrafts);
                        break;
                    case "4":
                        GroupByTree(aircrafts);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ошибка ввода!");
                        break;
                }
            }
        }

        static void MainMenu()
        {
            Console.WriteLine("\nВыберите библеотеку для работы:" +
                "\n1. 10 лабораторная" +
                "\n2. 12 лабораторная" +
                "\n0. Выход");
        }
        static void TenLibMenu()
        {
            Console.WriteLine("\nВыберите действие:" +
                "\n1. Where" +
                "\n2. Union" +
                "\n3. Min" +
                "\n4. GroupBy" +
                "\n5. Join" +
                "\n0. Выход в главное меню");
        }
        static void TwelveLibMenu()
        {
            Console.WriteLine("\nВыберите действие:" +
                "\n1. Where" +
                "\n2. Count" +
                "\n3. Min" +
                "\n4. GroupBy" +
                "\n0. Выход в главное меню");
        }
        static List<Aircraft> GenerateRandomAircrafts(int count)
        {
            var aircrafts = new List<Aircraft>();
            for (int i = 0; i < count; i++)
            {
                Aircraft name = new Aircraft();
                name.RandomInit();
                aircrafts.Add(name);
            }
            return aircrafts;
        }

        static int GetIntNum()
        {
            int num;
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
                return num;
            }
            catch
            {
                Console.WriteLine("Введено не целое число!");
                return -1;
            }
        }

        static void YearHigherThan1970(City city)
        {
            var newAircrafts = from airport in city.Airports
                               from aircraft in airport.Aircrafts
                               where aircraft.ProductionYear > 1970
                               select aircraft;
            Console.WriteLine("\nВоздушные судна произведенные после 1970 года (LINQ):");
            foreach (var aircraft in newAircrafts)
            {
                aircraft.Show();
            }
            var newAircrafts2 = city.Airports
                .SelectMany(x => x.Aircrafts)
                .Where(x => x.ProductionYear > 1970)
                .ToList();
            Console.WriteLine("\nВоздушные судна произведенные после 1970 года (Расш.):");
            newAircrafts2.ForEach(x => x.Show());
        }

        static void Union(City city, City city2)
        {
            var twoCitiesAircrafts = (from airport in city.Airports
                                      from aircraft in airport.Aircrafts
                                      select aircraft)
                                    .Union(from airport in city2.Airports
                                           from aircraft in airport.Aircrafts
                                           select aircraft)
                                    .ToList();
            Console.WriteLine("Среднее значение года производства вертолетов (LINQ):");
            Console.WriteLine(twoCitiesAircrafts);
            var twoCitiesAircrafts2 = city.Airports
                .SelectMany (x => x.Aircrafts)
                .Union(city2.Airports.SelectMany(airport => airport.Aircrafts))
                .ToList();
            Console.WriteLine("Среднее значение года производства вертолетов (Расшир.):");
            Console.WriteLine(twoCitiesAircrafts2);
                                 

        }

        static void Min(City city)
        {
            var minProductionYear = (from airport in city.Airports
                                     from aircraft in airport.Aircrafts
                                     select aircraft).Min();
            Console.WriteLine("Aircraft с min годом выпуска (LINQ):");
            Console.WriteLine(minProductionYear);

            var minProductionYear2 = city.Airports
                .SelectMany(x => x.Aircrafts)
                .Min();

            Console.WriteLine("Aircraft с min годом выпуска (Расшир.):");
            Console.WriteLine(minProductionYear2);
        }

        static void GroupBy(City city)
        {
            var GroupByType = (from airport in city.Airports
                               from aircraft in airport.Aircrafts
                               group aircraft by aircraft.Model);

            var GroupByType2 = city.Airports
                .SelectMany(airport => airport.Aircrafts)
                .GroupBy(a => a.Model);

            Console.WriteLine("GroupBy тип (LINQ):");
            foreach (var group in GroupByType)
            {
                foreach (var aircraft in group)
                {
                    Console.WriteLine($"{aircraft}");
                }
                Console.WriteLine($"Скорость {group.Key}: количество вагонов = {group.Count()}");
            }

            Console.WriteLine("GroupBy тип (Расшир.):");
            foreach (var group in GroupByType)
            {
                foreach (var aircraft in group)
                {
                    Console.WriteLine($"{aircraft}");
                }
                Console.WriteLine($"Скорость {group.Key}: количество вагонов = {group.Count()}");
            }
        }

        static void JoinAirportsAndAircrafts(List<City> cities)
        {
            foreach (var city in cities)
            {
                var joinedList = from airport in city.Airports
                                 from aircraft in airport.Aircrafts
                                 select new { AirportName = airport.Name, AircraftModel = aircraft.Model };

                Console.WriteLine($"Все самолеты города {city.Name} (LINQ):");
                foreach (var item in joinedList)
                {
                    Console.WriteLine($"Аэропорт: {item.AirportName}, Модель ВС: {item.AircraftModel}");
                }

                var joinedList2 = city.Airports
                    .SelectMany(airport => airport.Aircrafts,
                                (airport, aircraft) => new { AirportName = airport.Name, AircraftModel = aircraft.Model })
                    .ToList();

                Console.WriteLine($"Все самолеты города {city.Name} (Расшир.):");
                foreach (var item in joinedList2)
                {
                    Console.WriteLine($"Аэропорт: {item.AirportName}, Модель ВС: {item.AircraftModel}");
                }
            }
        }

        static void WhereTree(MyTree<Aircraft> aircrafts)
        {
            var whereMethod = aircrafts.Where(a => a.ProductionYear > 1970).ToList();
            var whereMethod2 = (from a in aircrafts where a.ProductionYear > 1970 select a).ToList();

            Console.WriteLine("Where (Расшир.):");
            whereMethod.ForEach(a => a.Show());
            Console.WriteLine("Where (LINQ):");
            whereMethod2.ForEach(a => a.Show());
        }

        static void ConutTree(MyTree<Aircraft> aircrafts)
        {
            var countMethod = aircrafts.Count(a => a.ProductionYear > 1970);
            var countMethod2 = (from a in aircrafts where a.ProductionYear > 1970 select a).Count();

            Console.WriteLine("Count (Расшир.):" +
                $"\n{countMethod}");
            Console.WriteLine("Count (LINQ):" +
                $"\n{countMethod2}");
        }

        static void MinTree(MyTree<Aircraft> aircrafts)
        {
            var minMethod = aircrafts.Min(a => a.ProductionYear);
            var minMethod2 = (from a in aircrafts select a.ProductionYear).Min();

            Console.WriteLine("Count (Расшир.LINQ):" +
                $"\n{minMethod}");
            Console.WriteLine("Count (LINQ):" +
                $"\n{minMethod2}");
        }

        static void GroupByTree(MyTree<Aircraft> aircrafts)
        {
            var groupMethod = aircrafts.GroupBy(a => a.Model).ToList();
            var groupMethod2 = (from a in aircrafts group a by a.Model).ToList();

            Console.WriteLine("GroupBy (Расшир.):");
            foreach (var group in groupMethod)
            {
                Console.WriteLine($"Speed {group.Key}: count = {group.Count()}");
            }
            Console.WriteLine("GroupBy (LINQ):");
            foreach (var group in groupMethod)
            {
                Console.WriteLine($"Speed {group.Key}: count = {group.Count()}");
            }
        }
    }
}
