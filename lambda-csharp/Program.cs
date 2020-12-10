using System;
using System.Globalization;
using System.Collections.Generic;
using Course.Entities;
using System.IO;
using System.Linq;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> list = new List<Employee>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: ");
                double moreSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Email of people whose salary is more than " + moreSalary.ToString("F2", CultureInfo.InvariantCulture) + ":");

                var emails = list.Where(p => p.Salary > moreSalary).OrderBy(p => p.Email).Select(p => p.Email);

                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }

                var sum = list.Where(p => p.Name[0] == 'M').Sum(p => p.Salary);

                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
