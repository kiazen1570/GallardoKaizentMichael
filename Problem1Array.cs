using System;

namespace KaiG.Problem1
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
            Student[] students = new Student[MaxStudents];
            int studentCount = 0;
            bool running = true;

            while (running)
            {
                Console.WriteLine("========================================");
                Console.WriteLine(" STUDENT RECORD MANAGEMENT");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStudent(students, ref studentCount); break;
                    case "2": DisplayAll(students, studentCount); break;
                    case "3": SearchStudent(students, studentCount); break;
                    case "4": UpdateStudent(students, studentCount); break;
                    case "5": DeleteStudent(students, ref studentCount); break;
                    case "6":
                        Console.WriteLine("Program exited.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1-6.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // Linear search: returns the index of the student, or -1 if not found.
        static int FindIndex(Student[] students, int count, string studentNumber)
        {
            for (int i = 0; i < count; i++)
            {
                if (students[i].StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        static int ReadYearLevel()
        {
            while (true)
            {
                Console.Write("Enter Year Level (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int year) && year >= 1 && year <= 4)
                    return year;
                Console.WriteLine("Invalid year level. Enter 1, 2, 3, or 4.");
            }
        }

        static void AddStudent(Student[] students, ref int count)
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
            if (FindIndex(students, count, number) != -1)
            {
                Console.WriteLine("A student with that Student Number already exists.");
                return;
            }

            Student s = new Student();
            s.StudentNumber = number;
            Console.Write("Enter Name: ");
            s.Name = Console.ReadLine().Trim();
            Console.Write("Enter Program: ");
            s.Program = Console.ReadLine().Trim();
            s.YearLevel = ReadYearLevel();

            students[count] = s;
            count++;
            Console.WriteLine("Student added successfully!");
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

        static void SearchStudent(Student[] students, int count)
        {
            Console.Write("Enter Student Number to search: ");
            int index = FindIndex(students, count, Console.ReadLine().Trim());
            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            Console.WriteLine("Student Found!");
            PrintStudent(students[index]);
        }

        static void UpdateStudent(Student[] students, int count)
        {
            Console.Write("Enter Student Number to update: ");
            int index = FindIndex(students, count, Console.ReadLine().Trim());
            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine("Leave Name/Program blank to keep the current value.");
            Console.Write("Enter new Name (" + students[index].Name + "): ");
            string name = Console.ReadLine().Trim();
            Console.Write("Enter new Program (" + students[index].Program + "): ");
            string program = Console.ReadLine().Trim();
            int year = ReadYearLevel();

            if (name != "") students[index].Name = name;
            if (program != "") students[index].Program = program;
            students[index].YearLevel = year;
            Console.WriteLine("Student updated successfully!");
        }

        static void DeleteStudent(Student[] students, ref int count)
        {
            Console.Write("Enter Student Number to delete: ");
            int index = FindIndex(students, count, Console.ReadLine().Trim());
            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            // Shift remaining records left to fill the gap.
            for (int i = index; i < count - 1; i++)
                students[i] = students[i + 1];
            students[count - 1] = new Student();
            count--;
            Console.WriteLine("Student deleted successfully!");
        }
    }
}
