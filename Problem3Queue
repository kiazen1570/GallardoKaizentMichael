using System;
using System.Collections.Generic;

namespace KaiG.Problem3
{
    struct StudentRequest
    {
        public string StudentNumber;
        public string StudentName;
        public string RequestType;
    }

    class Program
    {
        static void Main()
        {
            Queue<StudentRequest> requestQueue = new Queue<StudentRequest>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("========================================");
                Console.WriteLine(" STUDENT REQUEST QUEUE");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Request");
                Console.WriteLine("2. View Pending Requests");
                Console.WriteLine("3. Process Request");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddRequest(requestQueue); break;
                    case "2": ViewRequests(requestQueue); break;
                    case "3": ProcessRequest(requestQueue); break;
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

        static void AddRequest(Queue<StudentRequest> queue)
        {
            StudentRequest r = new StudentRequest();
            Console.Write("Enter Student Number: ");
            r.StudentNumber = Console.ReadLine().Trim();
            Console.Write("Enter Student Name: ");
            r.StudentName = Console.ReadLine().Trim();
            Console.Write("Enter Request Type: ");
            r.RequestType = Console.ReadLine().Trim();

            if (r.StudentNumber == "" || r.StudentName == "" || r.RequestType == "")
            {
                Console.WriteLine("All fields are required. Request not added.");
                return;
            }

            queue.Enqueue(r);   // joins the back of the line
            Console.WriteLine("Request added successfully!");
        }

        static void ViewRequests(Queue<StudentRequest> queue)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("There are no pending requests.");
                return;
            }

            Console.WriteLine("REQUEST QUEUE");
            int n = 1;
            // Enumerating a Queue goes from front (oldest) to back (newest) without removing anything.
            foreach (StudentRequest r in queue)
            {
                Console.WriteLine(n + ". " + r.StudentName + " - " + r.RequestType);
                n++;
            }
        }

        static void ProcessRequest(Queue<StudentRequest> queue)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("There are no pending requests to process.");
                return;
            }

            StudentRequest r = queue.Dequeue();   // removes the oldest request (FIFO)
            Console.WriteLine("Processing Request: " + r.StudentName + " - " + r.RequestType);
            Console.WriteLine("Request processed successfully!");
        }
    }
}
