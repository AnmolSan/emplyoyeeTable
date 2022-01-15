using System;
using System.Linq.Expressions;
using System.Data;
using System.Text;

namespace printTable
{
    class Program
    {
        static void Main(string[] args)
        {
            //Table.PrintTable();

            //while (Console.ReadKey().Key != ConsoleKey.E)
            //{
            //    try
            //    {
            //        Console.Write("\nEnter your number - ");
            //        var number = Convert.ToInt32(Console.ReadLine());

            //        Console.Write("\nEnter your second number - ");
            //        var number2 = Convert.ToInt32(Console.ReadLine());
            //        var calNum = new ConsoleCalculator();
            //        calNum.Calculate(number, number2);

            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("Please enter number");
                    
            //    }
                

                
            //    Console.WriteLine("Press any key to continue or press 'E' to Exit");
                
            //}

            //Console.Write("Enter you expression here - ");
            //var exprs = Console.ReadLine();
            ////Console.WriteLine(new DataTable().Compute(exprs, null));
            //Console.WriteLine(express.Evaluate(exprs));

            while (Console.ReadKey().Key != ConsoleKey.E)
            {
                
                Console.WriteLine(
                    "\nPlease Select the action you would like to take\n1 - Add Employee\n2 - Update Employee\n3 - Delete Employe\n4 - List Employee\n");
                var selection = Console.ReadLine();
                if (selection == "1")
                {
                    
                    Console.WriteLine("\nYou selected option 1 - Add Employee, please provide the following information");
                    Console.Write("Name - ");
                    var name  = Convert.ToString(Console.ReadLine());
                    Console.Write("ID no - ");
                    var id = int.Parse(Console.ReadLine());
                    Console.Write("DOB - ");
                    var dob = Convert.ToString(Console.ReadLine());
                    Employees.AddEmp(id,name,dob);
                }
                else if (selection == "2")
                {
                    Console.WriteLine("\nYou selected option 2 - Update Employee, please provide the Employee Number to update");
                    var id = int.Parse(Console.ReadLine());
                    Employees.UpdateEmp(id);
                }
                else if (selection == "3")
                {
                    Console.WriteLine("\nYou selected option 3 - Delete Employee, please provide the Employee Number to Delete");
                    var id = int.Parse(Console.ReadLine());
                    Employees.IsDelete(id,true);
                    
                }
                else if (selection == "4")
                {
                    Console.WriteLine("\nYou selected option 4 - List Employee");
                    Console.WriteLine("\nSearchby Name or Age (format - <A or >A)");
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                        Employees.ListEmployees();
                    else
                    {
                        Employees.ListEmployees(input);
                        Console.WriteLine("control");
                    }
                        
                        
                    
                }
                else
                {
                    Console.WriteLine("\nPlease Enter Number between 1 to 4");
                }
            }

            //int Expres(string exprs)
            //{
            //var mul = exprs.IndexOf("*");
            //var div = exprs.IndexOf("/");
            //var add = exprs.IndexOf("+");
            //var sub = exprs.IndexOf("-");
            //var temp = 0;
            //if(mul != -1)
            //    {
            //        var before = exprs[mul - 1];
            //        Console.WriteLine(before);
            //        var after = exprs[mul + 1];
            //        temp = before * after ;
            //        exprs.Remove(mul-1,3);

            //    }
            //if(div != -1)
            //    {   
            //        if(exprs.Length <= 2)
            //        {
            //            return exprs[0]/temp ;
            //        }
            //        var before = exprs[div - 1];
            //        var after = exprs[div + 1];
            //        temp = before * after;
            //        exprs.Remove(div - 1, 3);
            //    }


            //    return temp;
            //}





            //var opera = new char[] { '*','/','+','-'};
            //var num = exprs.split(opera);
            //foreach (var expr in num)
            //{
            //    console.writeline(expr);
            //}
            //console.writeline(num);



            //



        }
    }
}
