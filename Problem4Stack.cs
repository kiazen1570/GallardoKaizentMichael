using System;
using System.Collections.Generic;

namespace KaiG.Problem4
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    struct Operation
    {
        public string Action;          // "Added", "Updated", "Deleted"
        public string StudentNumber;
        public string StudentName;
    }

    class Program
    {
        const int MaxStudents = 10;

        static void Main()
        {
            Student[] students = new Student[MaxStudents];
            int studentCount = 0;
            Stack<Operation> operationHistory = new Stack<Operation>();

            bool running = true;
            while (running)
            {
                Console.WriteLine("========================================");
                Console.WriteLine(" STUDENT RECORDS WITH OPERATION HISTORY");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Display All Students");
                Console.WriteLine("5. View Operation History");
                Console.WriteLine("6. View Last Operation");
                Console.WriteLine("7. Remove Last Operation");
                Console.WriteLine("8. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStudent(students, ref studentCount, operationHistory); break;
                    case "2": UpdateStudent(students, studentCount, operationHistory); break;
                    case "3": DeleteStudent(students, ref studentCount, operationHistory); break;
                    case "4": DisplayAll(students, studentCount); break;
                    case "5": ViewHistory(operationHistory); break;
                    case "6": ViewLast(operationHistory); break;
                    case "7": RemoveLast(operationHistory); break;
                    case "8":
                        Console.WriteLine("Program exited.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1-8.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // ---------- Operation history (Stack, LIFO) ----------

        static void Record(Stack<Operation> history, string action, Student s)
        {
            Operation op = new Operation();
            op.Action = action;
            op.StudentNumber = s.StudentNumber;
            op.StudentName = s.Name;
            history.Push(op);   // newest operation goes on top
        }

        static void ViewHistory(Stack<Operation> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            Console.WriteLine("OPERATION HISTORY");
            // ToArray() returns top-of-stack first (newest -> oldest).
            // Loop backwards so the list reads oldest -> newest, as in the sample output.
            Operation[] ops = history.ToArray();
            int n = 1;
            for (int i = ops.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(n + ". " + ops[i].Action + " " + ops[i].StudentName);
                n++;
            }
        }

        static void ViewLast(Stack<Operation> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }
            Operation op = history.Peek();   // look at the top without removing it
            Console.WriteLine("Last Operation: " + op.Action + " " + op.StudentName);
        }

        static void RemoveLast(Stack<Operation> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }
            history.Pop();   // removes the most recent operation
            Console.WriteLine("Last operation removed successfully!");
        }

        // ---------- Student records (array of struct, from Problem 1) ----------

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

        static void AddStudent(Student[] students, ref int count, Stack<Operation> history)
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
            Record(history, "Added", s);
            Console.WriteLine("Student added successfully!");
        }

        static void UpdateStudent(Student[] students, int count, Stack<Operation> history)
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

            Record(history, "Updated", students[index]);
            Console.WriteLine("Student updated successfully!");
        }

        static void DeleteStudent(Student[] students, ref int count, Stack<Operation> history)
        {
            Console.Write("Enter Student Number to delete: ");
            int index = FindIndex(students, count, Console.ReadLine().Trim());
            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Student removed = students[index];
            for (int i = index; i < count - 1; i++)
                students[i] = students[i + 1];
            students[count - 1] = new Student();
            count--;

            Record(history, "Deleted", removed);
            Console.WriteLine("Student deleted successfully!");
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
                Console.WriteLine("Student Number: " + students[i].StudentNumber);
                Console.WriteLine("Name: " + students[i].Name);
                Console.WriteLine("Program: " + students[i].Program);
                Console.WriteLine("Year Level: " + students[i].YearLevel);
                Console.WriteLine();
            }
        }
    }
}
