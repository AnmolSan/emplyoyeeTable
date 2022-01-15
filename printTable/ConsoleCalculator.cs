using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace printTable
{
    class ConsoleCalculator
    {
        public int Number;
        public int Number2;
        
        public void Calculate(int num1, int num2)
        {
            this.Number = Convert.ToInt32(num1);
            this.Number2 = Convert.ToInt32(num2);    
            Console.WriteLine("Press 1 to Multiply\nPress 2 to divide\nPress 3 to Add\nPress 4 to subtract");
            var operation = Convert.ToInt32(Console.ReadLine());

            switch(operation)
            {
                case 1:
                    Console.WriteLine("multipling...\n{0}",Number*Number2);
                    break;
                case 2:
                    if (Number2 == 0)
                    {
                        Console.WriteLine("Divisor cannot be zero");
                        break;
                    }
                    Console.WriteLine("Dividing...\n{0}", Number / Number2);
                    break;
                case 3:
                    Console.WriteLine("Adding...\n{0}",Number+Number2);
                    break;
                case 4:
                    Console.WriteLine("Subtracting...\n{0}", Number - Number2);
                    break;
                default:
                    Console.WriteLine("Enter the number Between 1 to 4");
                    break;
            }


        }
    }
}
