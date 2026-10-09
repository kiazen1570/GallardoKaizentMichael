using System;
using System.Collections.Generic;

namespace KaiG.Problem2
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    class Program
    {
        const int MaxStudents = 10;

        static void Main()
        {
            // Problem 1's storage is kept (array of struct, max 10)...
            Student[] students = new Student[MaxStudents];
            int studentCount = 0;

            // ...and the Dictionary is used for fast lookup: Student Number -> Student Record
            Dictionary<string, Student> studentDictionary = new Dictionary<string, Student>();

            bool running = true;
            while (running)
            {
                Console.WriteLine("========================================");
                Console.WriteLine(" STUDENT LOOKUP USING DICTIONARY");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Search Student");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent(students, ref studentCount, studentDictionary);
                        break;
                    case "2":
                        SearchStudent(studentDictionary);
                        break;
                    case "3":
                        DisplayAll(students, studentCount);
                        break;
                    case "4":
                        Console.WriteLine("Program exited.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1-4.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void AddStudent(Student[] students, ref int count, Dictionary<string, Student> dict)
        {
            if (count >= MaxStudents)
            {
                Console.WriteLine("Cannot add student. The record is full (maximum of 10 students).");
                return;
            }

            Console.Write("Enter Student Number: ");
            string number = Console.ReadLine().Trim();
            if (number == "")
            {
                Console.WriteLine("Student Number cannot be empty.");
                return;
            }
            // Duplicate prevention using the dictionary
            if (dict.ContainsKey(number))
            {
                Console.WriteLine("Duplicate Student Number! A student with that number already exists.");
                return;
            }

            Student s = new Student();
            s.StudentNumber = number;
            Console.Write("Enter Name: ");
            s.Name = Console.ReadLine().Trim();
            Console.Write("Enter Program: ");
            s.Program = Console.ReadLine().Trim();

            while (true)
            {
                Console.Write("Enter Year Level (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int year) && year >= 1 && year <= 4)
                {
                    s.YearLevel = year;
                    break;
                }
                Console.WriteLine("Invalid year level. Enter 1, 2, 3, or 4.");
            }

            students[count] = s;
            count++;
            dict.Add(s.StudentNumber, s);   // key = Student Number, value = Student record
            Console.WriteLine("Student added successfully!");
        }

        static void SearchStudent(Dictionary<string, Student> dict)
        {
            Console.Write("Enter Student Number to search: ");
            string number = Console.ReadLine().Trim();

            // TryGetValue: one O(1) lookup, no exception if the key is missing
            if (dict.TryGetValue(number, out Student s))
            {
                Console.WriteLine("Student Found!");
                PrintStudent(s);
            }
            else
            {
                Console.WriteLine("Student Number not found.");
            }
        }

        static void PrintStudent(Student s)
        {
            Console.WriteLine("Student Number: " + s.StudentNumber);
            Console.WriteLine("Name: " + s.Name);
            Console.WriteLine("Program: " + s.Program);
            Console.WriteLine("Year Level: " + s.YearLevel);
        }

        static void DisplayAll(Student[] students, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("No student records found.");
                return;
            }
            Console.WriteLine("========================================");
            Console.WriteLine(" STUDENT RECORDS");
            Console.WriteLine("========================================");
            for (int i = 0; i < count; i++)
            {
                PrintStudent(students[i]);
                Console.WriteLine();
            }
        }
    }
}
