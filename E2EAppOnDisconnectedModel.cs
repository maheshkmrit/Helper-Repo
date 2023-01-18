using SampleDataAccessApp.Practical.Dalayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace SampleDataAccessApp.Practical
{
   

    namespace Dalayer
    {
        interface IDataAccessComponent
        {
            bool AddNewEmployee(string name, string address, int salary, int deptId);
            bool UpdateEmployee(int recId, string name, string address, int salary, int deptId);
            void DeleteEmployee(int id);
            void GetAllEmployees();
            //void GetAllDepts();
        }
        class DataComponent : IDataAccessComponent
        {
            static string strConnection = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            const string query = "SELECT * FROM TBLEMPLOYEE; SELECT * FROM TBLDEPT";
            static DataSet disconnectedObj = new DataSet("All Records");
            static SqlDataAdapter ada = null;



            public static void fillRecords()
            {
                ada = new SqlDataAdapter(query, strConnection);
                SqlCommandBuilder sb = new SqlCommandBuilder(ada);
                ada.Fill(disconnectedObj);
                disconnectedObj.Tables[0].TableName = "EmpList";
                disconnectedObj.Tables[1].TableName = "DeptList";
                Trace.WriteLine("Connection State: " + ada.SelectCommand.Connection.State);
            }

            public bool UpdateEmployee(int recId, string name, string address, int salary, int deptId)
            {
                foreach(DataRow row in disconnectedObj.Tables[0].Rows)
                {
                    if((int)(row[0]) == recId)
                    {

                        row[1] = name;
                        row[2] = address;
                        row[3] = salary;
                        row[4] = deptId;

                        ada.Update(disconnectedObj, "EmpList");
                        return true;
                    }
                }
                return false;
            }



            private DataTable GetRecords()
            {
                return null;
                //throw new Exception("UnImplemented");
            }


            public bool AddNewEmployee(string name, string address, int salary, int deptId)
            {
              
                
                DataRow newRow = disconnectedObj.Tables[0].NewRow();
                newRow[1] = name;
                newRow[2] = address;
                newRow[3] = salary;
                newRow[4] = deptId;
                disconnectedObj.Tables[0].Rows.Add(newRow);
                int res = ada.Update(disconnectedObj, "EmpList");
                return (res > 0) ? true : false;
            }

            public void DeleteEmployee(int id)
            {
                foreach (DataRow row in disconnectedObj.Tables[0].Rows)
                {
                    
                    if ((int)row[0] == id)
                    {
                        row.Delete();
                        break;
                    }
                }
                ada.Update(disconnectedObj, "EmpList");
            }

            public void GetAllDepts()
            {
                //throw new Exception("UnImplemented");
                
            }

            public void GetAllEmployees()
            {
                DataComponent.fillRecords();
                foreach (DataRow row in disconnectedObj.Tables[0].Rows)
                {
                    Console.WriteLine(row[1]);
                }

            }

           

        }
    }

    namespace UILayer
    {
        using SimpleFrameWorkApp;
        using System.Configuration;

       
        class MainRunner
        {
            static IDataAccessComponent component = new DataComponent();

            
            static void InsertHelper()
            {
                DataComponent.fillRecords();
                string name = Utilities.Prompt("Enter Employee name ");
                string loc = Utilities.Prompt("Enter Address ");
                int salary = Utilities.GetNumber("Enter salary");
                int deptId = Utilities.GetNumber("Enter Employee Department Id");
                bool res = component.AddNewEmployee(name,loc, salary, deptId);
                if (res)
                    Console.WriteLine("Employee Added Successful!");
                else
                    Console.WriteLine("Failed to Add Employee!");

            }

            static void DeleteHelper()
            {
                DataComponent.fillRecords();
                int id = Utilities.GetNumber("Enter Employee Id to Delete: ");
                component.DeleteEmployee(id);
                Console.WriteLine( "Emloyee Deleted Successful");
                Utilities.Prompt("Press Enter to clear Console");
                Console.Clear();
            }

            static void GetAllEmployeeHelper()
            {
                component.GetAllEmployees();
                Utilities.Prompt("Press Enter to clear Console");
                Console.Clear();
            }

            static void UpdateEmployeeHalper()
            {
                DataComponent.fillRecords();
                int recId = Utilities.GetNumber("Enter Employee Id to update");
                string name = Utilities.Prompt("Enter name to update: ");
                string address = Utilities.Prompt("Enter address to update: ");
                int salary = Utilities.GetNumber("Enter salary to update: ");
                int deptId = Utilities.GetNumber("Enter Department Id to update");
                bool response = component.UpdateEmployee(recId, name, address, salary, deptId);


            }

            public static void MyUI()
            {
                string MenuHeader = "--------------------------------Employee Manager Application------------------------------------\t\t\t\n";
                string Menu = "\t\t\tPress 1 ------------------- to Add Employee\n" +
                  "\t\t\tPress 2 ------------------- to View All Employee\n" +
                  "\t\t\tPress 3 ------------------- to Delete Employee\n" +
                  "\t\t\tPress 4 ------------------- to Update Employee\n" +
                  "\t\t\tPress x ------------------- to Exit";

                bool res = true;

                while (res)
                {
                    Console.WriteLine(MenuHeader);
                    Console.WriteLine(Menu);
                RETRY:
                    string choice = Console.ReadLine().ToLower();
                    switch (choice)
                    {
                        case "1":
                            InsertHelper();
                            break;
                        case "2":
                            GetAllEmployeeHelper();
                            break;
                        case "3":
                            DeleteHelper();
                            break;
                        case "4":
                            UpdateEmployeeHalper();
                            break;
                        case "x":
                            res = false;
                            break;
                        default:
                            break;
                    }

                }
            }

            static void Main(string[] args)
            {
                MyUI();
               

            }


        }

    }
}
