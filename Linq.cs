using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
/*
 * LINQ stands for Language Integrated Queries. 
 * LINQ allows to perform SQL like Queries on Collection objects of .NET.
 * System.Linq is the namespace for working with Collections. 
 * With this namespace, u get new keywords of query like from, in, where orderby group by, join, select for performing queries. 
 * The Query executes when the iteration happens. 
 */
namespace SampleDataAccessApp
{
    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCity { get; set; }
        public int EmpSalary { get; set; }
        public int DeptId { get; set; }
    }

    class Dept
    {
        public string DeptName { get; set; }
        public int DeptId { get; set; }
    }
    static class DataComponent
    {
        const string fileName = "SampleData.csv";
        private static List<Employee> getAll()
        {
            var list = new List<Employee>();
            var lines = File.ReadAllLines(fileName);
            foreach(var line in lines)
            {
                var words = line.Split(',');
                var newEmp = new Employee
                {
                    EmpId = int.Parse(words[0]),
                    EmpName = words[1],
                    EmpCity = words[2],
                    EmpSalary = int.Parse(words[3]),
                    DeptId = int.Parse(words[4])
                };
                list.Add(newEmp);
            }
            return list;
        }
        public static List<Employee> AllRecords => getAll();

        public static List<Dept> AllDepts => new List<Dept>
        {
            new Dept{ DeptId = 1, DeptName ="Admin"},
            new Dept{ DeptId = 2, DeptName ="Sales"},
            new Dept{ DeptId = 3, DeptName ="Utilities"},
            new Dept{ DeptId = 4, DeptName ="Testing"},
            new Dept{ DeptId = 5, DeptName ="Development"},
            new Dept{ DeptId = 6, DeptName ="IT Help"}
        };
    }

    class LINQProgram
    {
        static List<Employee> data = DataComponent.AllRecords;
        static List<Dept> depts = DataComponent.AllDepts;
        static void Main()
        {
            //displayAllNames();
            //displayNamesAndAddresses();
            //displayNamesFromCity("Mysore");
            //displayNamesWithSalariesGreaterThan(300000);
            //displayNamesOrderbyName();
            //displayUniqueCities();
            //displayMinMaxAvgSalaries();
            //GetAllEmpNames whose salary is greater than the Avg Salary.....
            //displayEmployeesAboveAvgSal();
            //displayNamesGroupedByCity();
            //displayGroupedByAlphabet();
            //displayEmpNameAndDept();
            displayDeptGroups();
        }

        private static void displayDeptGroups()
        {
            var groups = from dept in depts
                         join emp in data on dept.DeptId equals emp.DeptId
                         group new { Name = emp.EmpName, Dept = dept.DeptName }
                         by dept.DeptName into g
                         select g;
            foreach(var group in groups)
            {
                Console.WriteLine("Employees under " + group.Key);
                foreach(var name in group)
                    Console.WriteLine(name);
                Console.WriteLine();
            }
        }

        private static void displayEmpNameAndDept()
        {
            var query = from emp in data
                        join dept in depts on emp.DeptId equals dept.DeptId
                        select new { Name = emp.EmpName, Dept = dept.DeptName };
            foreach(var res in query)
            {
                Console.WriteLine(res);
            }
        }

        private static void displayGroupedByAlphabet()
        {
            var groups = from emp in data
                         group emp.EmpName by emp.EmpName[0] into gr
                         orderby gr.Key ascending
                         select gr;
            foreach (var group in groups)
            {
                Console.WriteLine("People from " + group.Key);
                foreach (var name in group)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }

        private static void displayNamesGroupedByCity()
        {
            var groups = from emp in data
                         group emp.EmpName by emp.EmpCity into gr
                         orderby gr.Key ascending
                         select gr;
            foreach(var group in groups)
            {
                Console.WriteLine("People from " + group.Key);
                foreach(var name in group)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }

        private static void displayEmployeesAboveAvgSal()
        {
            var empNames = from rec in data
                           where rec.EmpSalary > data.Average((emp)=>emp.EmpSalary)
                           select rec;
            foreach(var emp in empNames)
                Console.WriteLine("{0} earns {1:c}", emp.EmpName, emp.EmpSalary);
        }

        private static void displayMinMaxAvgSalaries()
        {
            var maxSalary = (from rec in data
                             select rec.EmpSalary).Max();
            var minSalary = (from rec in data
                             select rec.EmpSalary).Min();
            var avgSalary = (from rec in data
                             select rec.EmpSalary).Average();
            Console.WriteLine("The Min Salary: {0}\nThe Max Salary:{1}\nThe AvgSalary: {2}", minSalary, maxSalary, avgSalary);
        }

        private static void displayUniqueCities()
        {
            var query = (from rec in data
                         select rec.EmpCity).Distinct();
            foreach(var cityName in query)
                Console.WriteLine(cityName);
        }

        private static void displayNamesOrderbyName()
        {
            var query = from rec in data
                        orderby rec.EmpName descending
                        select rec.EmpName;
            foreach(var name in query)
                Console.WriteLine(name);
        }

        private static void displayNamesWithSalariesGreaterThan(int salary)
        {
            var query = from rec in data
                        where rec.EmpSalary >= salary && rec.EmpName.StartsWith("S")
                        select new { rec.EmpName, rec.EmpSalary };
            foreach (var name in query)
                Console.WriteLine(name);
        }

        //Display names of employees whose salary is more than 50000....
        private static void displayNamesFromCity(string city)
        {
            var query = from rec in data
                        where rec.EmpCity == city
                        select rec.EmpName;
            foreach(var name in query)
                Console.WriteLine(name);
        }

        private static void displayNamesAndAddresses()
        {
            var query = from rec in data
                        select new { Name = rec.EmpName, Address = rec.EmpCity };
            foreach(var res in query)
                Console.WriteLine($"{res.Name} from {res.Address}");
        }

        private static void displayAllNames()
        {
            var query = from emp in data
                        select emp.EmpName;
            foreach(var name in query)
                Console.WriteLine(name.ToUpper());
        }
    }
}
