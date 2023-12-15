using System;
using System.Collections.Generic;

namespace ExamCS2
{
    class Tariff
    {
        public string Direction { get; set; }
        public double Price { get; set; }
    }

    class Passenger
    {
        public string PassportData { get; set; }
        public string Direction { get; set; }
    }

    class Airport
    {
        private List<Tariff> tariffs = new List<Tariff>();
        private List<Passenger> passengers = new List<Passenger>();

        public void AddTariff(string direction, double price)
        {
            tariffs.Add(new Tariff { Direction = direction, Price = price });
            Console.WriteLine($"Тариф для напрямку {direction}" +
                                            $" додано успішно.");
        }

        public void RegisterTicket(string passportData, string direction)
        {
            Tariff tariff = tariffs.Find(t => t.Direction == direction);

            if (tariff != null)
            {
                passengers.Add(new Passenger { PassportData = passportData,
                                                  Direction = direction });
                Console.WriteLine($"Квиток за напрямком {direction} " +
                    $"зареєстровано для пасажира {passportData}.");
            }
            else
            {
                Console.WriteLine($"Тариф для напрямку {direction} не знайдено.");
            }
        }

        public double CalculateTicketCost(string direction)
        {
            double totalCost = 0;

            foreach (Passenger passenger in passengers)
            {
                if (passenger.Direction == direction)
                {
                    Tariff tariff = tariffs.Find(t => t.Direction == direction);
                    totalCost += tariff?.Price ?? 0;
                }
            }

            return totalCost;
        }

        public double CalculateTotalRevenue()
        {
            double totalRevenue = 0;

            foreach (Passenger passenger in passengers)
            {
                Tariff tariff = tariffs.Find(t => t.Direction == passenger.Direction);
                totalRevenue += tariff?.Price ?? 0;
            }

            return totalRevenue;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Airport airport = new();

            Console.WriteLine("Вітаємо в системі керування аеропортом!");
            Switcher(airport);

        }
        static void Switcher(Airport airport)
        {
            Console.WriteLine("Оберіть потрібну операцію:");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                default:
                    Console.WriteLine("Ця операція не існує. " +
                                                "Повторіть спробу.");
                    Back(airport);
                    break;
                case 0:
                    Console.WriteLine("Дякуємо за користування утилітою.");
                    break;
                case 1:
                    airport.AddTariff("По Україні", 100);
                    airport.AddTariff("Міжнародний", 200);
                    Back(airport);
                    break;
                case 2:
                    airport.RegisterTicket("AB123456", "По Україні");
                    airport.RegisterTicket("CD789012", "Міжнародний");
                    airport.RegisterTicket("EF345678", "По Україні");
                    Back(airport);
                    break;
                case 3:
                    double costDomestic = airport.CalculateTicketCost("По Україні");
                    double costInternational = airport.CalculateTicketCost("Міжнародний");
                    Console.WriteLine($"Сукупна вартість квитків " +
                        $"перельотів по Україні: {costDomestic}");
                    Console.WriteLine($"Сукупна вартість квитків " +
                        $"міжнародних перельотів: {costInternational}");
                    Back(airport);
                    break;
                case 4:
                    double totalRevenue = airport.CalculateTotalRevenue();
                    Console.WriteLine($"Загальна виручка: {totalRevenue}");
                    Back(airport);
                    break;
            }
        }
        static void Back(Airport airport)
        {
            Console.WriteLine("Натисніть довільну клавішу, щоб продовжити.");
            Console.ReadKey();
            Console.Clear();
            Switcher(airport);
        }
    }
}