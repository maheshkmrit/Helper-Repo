using System;
public class UI 
{
  public static void MyUI() 
  {
    string MenuHeader = "--------------------------------Patient Manager Application------------------------------------\t\t\t\n";
    string Menu = "\t\t\tPress 1 ------------------- to Add Patient\n" +
      "\t\t\tPress 2 ------------------- to View All Patient\n" +
      "\t\t\tPress 3 ------------------- to Delete Patient\n" +
      "\t\t\tPress 4 ------------------- to Update Patient\n" +
      "\t\t\tPress x ------------------- to Exit";

    bool res = true;

    while (res) {
      Console.WriteLine(MenuHeader);
      Console.WriteLine(Menu);
      RETRY:
        string choice = Console.ReadLine().ToLower();
      switch (choice) {
      case "1":
        Console.WriteLine("Do Something");
        break;
      case "2":
        Console.WriteLine("Do Something");
        break;

      case "3":
        Console.WriteLine("Do Something");
        break;
      case "4":
        Console.WriteLine("Do Something");
        break;
      case "x":
        res = false;
        break;
      default:
        break;
      }

    }
  }

  static void Main(string[] args) {
    MyUI();
  }
}
