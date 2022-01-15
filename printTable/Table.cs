using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace printTable
{
    class Table
    {
        public static void PrintTable()
        {

            while (true)
            {
                Console.Write("\nEnter a number to print table (0 to exit) - ");
                var number = Convert.ToInt32(Console.ReadLine());
                if (number != 0)
                {
                    Console.WriteLine("\n\nTable of {0}\n", number);
                    for (int i = 1; i <= 10; i++)
                    {
                        Console.WriteLine("{0} X {1} = {2}", number, i, number * i);
                    }
                }
                else
                {
                    Console.WriteLine("\nThank you for using Table");
                    break;
                }
            }
        }
    }
}
