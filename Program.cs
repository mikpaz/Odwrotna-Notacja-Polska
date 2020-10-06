using System;
using System.Collections.Generic;

namespace I_02_ONP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Menu");
            string choice = "";
            string equation = "";
            while (choice != "q")
            {
                Console.WriteLine("1    - convert from classic notation to ONP");
                Console.WriteLine("2    - convert from ONP to classic notation");
                Console.WriteLine("q    - quit program");
                Console.Write(">> ");
                choice = Console.ReadLine();
                if(choice == "1")
                {
                    Console.WriteLine("Input the equation you want to convert: ");
                    Console.Write(">> ");
                    equation = Console.ReadLine();
                    Console.WriteLine("Output: " + classicToONP(equation));
                }
                else if(choice == "2")
                {
                    Console.WriteLine("Input the equation you want to convert: ");
                    Console.Write(">> ");
                    equation = Console.ReadLine();
                    Console.WriteLine("Output: " + ONPtoClassic(equation));
                }
                else if (choice != "q")
                    Console.WriteLine("Try again");
            }
        }

        static string classicToONP(string characters)
        {
            Stack<char> stack = new Stack<char>();
            string result = "";

            for (int i = 0; i < characters.Length; ++i)
            {
                char c = characters[i];
  
                if (char.IsLetterOrDigit(c))
                {
                    result += c;
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }

                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        result += stack.Pop();
                    }

                    if (stack.Count > 0 && stack.Peek() != '(')
                    {
                        return "Invalid Expression";
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    while (stack.Count > 0 && setPriority(c) <= setPriority(stack.Peek()))
                    {
                        result += stack.Pop();
                    }
                    stack.Push(c);
                }

            }

            while (stack.Count > 0)
            {
                result += stack.Pop();
            }

            return result;
        }

        static string ONPtoClassic(string characters)
        {
            Stack<string> s = new Stack<string>();

            for (int i = 0; i < characters.Length; i++)
            {
                
                if (isOperand(characters[i]))
                {
                    s.Push(characters[i] + "");
                }

                else
                {
                    String op1 = (String)s.Peek();
                    s.Pop();
                    String op2 = (String)s.Peek();
                    s.Pop();
                    s.Push("(" + op2 + characters[i] + op1 + ")");
                }
            }

            return (String)s.Peek();
        }

        static Boolean isOperand(char x)
        {
            return (x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z');
        }

        static int setPriority(char sign)
        {
            switch (sign)
            {
                case '+':
                    return 1;
                case '-':
                    return 1;
                case '*':
                    return 2;
                case '/':
                    return 2;
                case '^':
                    return 3;
            }
            return -1;
        }
    }
}
