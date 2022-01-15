using System;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections.Generic;

namespace printTable;

class express
{
    public static int Evaluate(string expression)
    {
        var token = expression.ToCharArray();

        //operands values stacked 
        Stack<int> valuesStack = new Stack<int>();

        //operators stack
        Stack<char> operatorStack = new Stack<char>();

        for (int i = 0; i < token.Length; i++)
        {
            //current token is whitespace skip it 
            if (token[i] == ' ')
            {
                continue;
            }

            //if current token is the number, push it into stack 
            if (token[i]>='0' && token[i] <='9')
            {
                var strBuff = new StringBuilder();

                //for more than one digit of number 
                while (i<token.Length && token[i] >='0' && token[i] <= '9')
                {
                    strBuff.Append(token[i++]);
                }
                //converting the string builder to int then pushing it into the Stack of value
                valuesStack.Push(int.Parse(strBuff.ToString()));

                // To decreament the counter increamented by for loop 
                i--;
            }
            //if current token is an open bracket
            else if (token[i] == '(')
            {
                operatorStack.Push(token[i]);
            }
            //if current token is an closing bracket 
            else if (token[i] == ')')
            {
                while (operatorStack.Peek() != '(')
                {
                    valuesStack.Push(ApplyOperator(operatorStack.Pop(), valuesStack.Pop(), valuesStack.Pop()));

                }

                operatorStack.Pop();

            }

            //if current token is an operator
            else if (token[i] == '+'|| token[i] == '-' || token[i] == '*' || token[i] == '/')
            {
                while (operatorStack.Count >= 1 && HasPrecedence(token[i],operatorStack.Peek()))
                {
                    valuesStack.Push(ApplyOperator(operatorStack.Pop(), valuesStack.Pop(), valuesStack.Pop()));
                }
                //pushing the current operator to operatorStack 
                operatorStack.Push(token[i]);
            }

        }
        while (operatorStack.Count > 0)
        {
            valuesStack.Push(ApplyOperator(operatorStack.Pop(), valuesStack.Pop(), valuesStack.Pop()));
        }


        return valuesStack.Pop();
    }

    public static bool HasPrecedence(char currentIterationOperator, char lastIterationOperator)
    {
        if (currentIterationOperator == '('|| lastIterationOperator == ')')
        {
            return false;
        }

        if ((currentIterationOperator == '*' || currentIterationOperator == '/') && (lastIterationOperator == '-' || lastIterationOperator == '+'))
        {
            return false;
        }

        return true;
    }

    public static int ApplyOperator(char operat, int num1, int num2)
    {
        switch (operat)
        {
            case '+':
                return num1 + num2;
            case '-':
                return num1 - num2;
            case '*':
                return num1 * num2;
            case '/':
                if (num2 == 0)
                {
                    throw new System.NotSupportedException("Divisor cannot be zero");
;               }

                return num1 / num2;

        }

        return 0;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    ////to check isOperand or not 
    //static bool isOperand(char c)
    //{
    //    return (c >= '0' && c <= '9');
    //}


    //static int value(char c)
    //{
    //    return (int)(c- '0');
    //}

    //public static int Evaluate(string exp)
    //{   
    //    if (exp.Length == 0) return -1;

    //    //first operand
    //    var result = value(exp[0]);

    //    //traversing to remaining char 
    //    for (int i = 1; i < exp.Length; i+=2)
    //    {
    //        //next char must be an operator 
    //        var opert = exp[i];
    //        var operand = exp[i + 1];

    //        //to check the next char to be an operand 
    //        if (isOperand(operand) == false) return -1;

    //        if (opert == '+') result += value(operand);
    //        else if (opert == '-') result -= value(operand);
    //        else if (opert == '*') result *= value(operand);
    //        else if (opert == '/') result /= value(operand);

    //        //if expression is invalid 
    //        else return -1;
            
    //    }

    //    return result;
    //}




}