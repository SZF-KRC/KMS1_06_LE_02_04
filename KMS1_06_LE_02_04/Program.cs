using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMS1_06_LE_02_04
{
    internal class Program
    {
        static List<(string Name, string Surname, int Age, string Phone)> personalData = new List<(string,string,int,string)>();
        static void Main(string[] args)
        {
            printMenu();
        }

        static void printMenu() 
        {
            int userInput;
            bool exit = false;          
            while (!exit)
            {
                Console.Write("\n*** Verwaltung von Personendaten ***\n[1] Anlegen einer Person\n[2] Anzeigen von Personen nach Alter\n[3] Anzeigen aller Einträge\n[4] Program beenden\nGeben Sie den Index Ihrer Wahl ein: ");
                userInput = InputNumber();
                switch (userInput)
                {
                    case 1:
                        if(personalData.Count == 10)
                        {
                            Console.WriteLine("\n°°° Das System umfasst bereits 10 Personen °°°\n");
                        }
                        else
                        {
                            AddPerson();
                        }                      
                        break;
                    case 2:
                        PrintPeopleByAge();
                        break;
                    case 3:
                        PrintPeople();                     
                        break;
                    case 4:
                        exit = true;
                        Console.WriteLine("Program beenden");
                        break;
                    default: Console.WriteLine("\n--- Geben Sie nur den Index von 1-4 ein ---\n");
                        break;
                }
            }
        }


        static void AddPerson()
        {
            string name, surname, phone;
            int age;
            Console.Write("\n*** Anlegen einer Person ***\nVorname eingeben: ");
            name = InputString();
            Console.Write("Nachname eingeben: ");
            surname = InputString();
            Console.Write("Alter eingeben: ");
            age = InputAge();
            Console.Write("Teleffonnumer eingeben: ");
            phone = InputPhone();
            personalData.Add((name,surname, age, phone));
        }

        static void PrintPeopleByAge()
        {
            int age, personCount = 1;
            if (IsPersonInList())
            {
                Console.Write("\n*** Anzeigen von Personen nach Alter (z.B. alle Personen unter 30 Jahren) ***\nAlter eingeben: ");
                age = InputNumber();

                foreach (var person in personalData)
                {
                    if (person.Age < age)
                    {
                        Console.WriteLine($"{personCount} Person: {person.Name} {person.Surname}, Alter: {person.Age}, Teleffonummer: {person.Phone}");
                        personCount++;
                    }
                }
                if (personCount == 1) 
                {
                    Console.WriteLine($"\n--- Keine Personen unter {age} Jahren ---\n");
                }
            } 
        }

        static void PrintPeople()
        {
            if (IsPersonInList())
            {
                int personCount = 1;
                foreach (var person in personalData)
                {
                    Console.WriteLine($"{personCount} Person: {person.Name} {person.Surname}, Alter: {person.Age}, Teleffonummer: {person.Phone}");
                    personCount++;
                }
            }
        }

        static bool IsPersonInList()
        {
            if(personalData.Count == 0)
            {
                Console.WriteLine("\n--- Keine personenbezogenen Daten verfügbar ---\n");
                return false;
            }
            return true;
        }

        static int InputAge()
        {
            while (true)
            {
                int age = InputNumber();
                if (age > 0 && age < 120)
                {
                   return age; 
                }
                else
                {
                    Console.WriteLine("\n--- Das Alter kann zwischen null und 120 Jahren liegen ---\n");
                }
            }
        }

        static int InputNumber()
        {
            int number;
            while (true)
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    break;
                }catch (Exception e)
                {
                    Console.WriteLine("\n--- Geben Sie nur eine Ganzzahl ein ---\n");
                }
            }
            return number;
        }
        static string InputString()
        {
            bool exit= false;
            string input = "";
            while (!exit)
            {
                input = Console.ReadLine();
                if (input.Any(char.IsDigit))
                {
                    Console.WriteLine("\n--- Ungültige Eingabe. Schreiben Sie nur Alphabete ---\n");
                }else if (input.Length < 1)
                {
                    Console.WriteLine("\n--- Die Eingabe darf nicht leer sein ---\n");
                }else
                {
                    exit = true;
                }
            }
            return input;
        }
        public const string mobilRegex = @"^\+?\d{1,3} ?\d{3} ?\d{3} ?\d{3,4}$";

        static string InputPhone()
        {
            string phoneNumber;
            while (true)
            {
                phoneNumber = Console.ReadLine();
                if(phoneNumber.Length > 0 && Regex.IsMatch(phoneNumber,mobilRegex))
                {
                    return phoneNumber;
                }else
                {
                    Console.WriteLine("\n--- Ungültige Telefonnummer (z. B. +43 660 666 111) ---\n");
                }
            }
        }
    }
}
