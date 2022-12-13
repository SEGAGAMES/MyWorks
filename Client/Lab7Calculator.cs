using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Calculator
    {
        public Calculator()
        {
        }

        public double Calc(string input)
        {
            input = input.Replace(" ", "");
            input = pretreatment(input);
            List<string> tokens = tokenizing(input);
            List<string> rpn = shuntingYard(tokens);


            Stack<double> stack = new Stack<double>();
            double number;
            foreach (string op in rpn)
            {
                if (double.TryParse(op, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    double secondOp = stack.Pop();
                    switch (op)
                    {
                        case "*": stack.Push(stack.Pop() * secondOp); break;
                        case "/": stack.Push(stack.Pop() / secondOp); break;
                        case "%": stack.Push(stack.Pop() % secondOp); break;
                        case "+": stack.Push(stack.Pop() + secondOp); break;
                        case "-": stack.Push(stack.Pop() - secondOp); break;
                        case "p": stack.Push(+secondOp); break;
                        case "m": stack.Push(-secondOp); break;
                        case "!":
                            if ((long)secondOp == secondOp && secondOp >= 0)
                                stack.Push(factorial((long)secondOp));
                            else
                                throw new Exception($"Не предусмотрено вычисление факториала для числа '{secondOp}'");
                            break;
                    }
                }
            }

            return stack.Peek();
        }

        string pretreatment(string input)
        {
            StringBuilder outString = new StringBuilder();
            bool nextpmIsUnar = true;
            for (int i = 0; i < input.Length; i++)
            {
                if ("-+".Contains(input[i]) && nextpmIsUnar)
                {
                    if (input[i] == '+') outString.Append("p");
                    if (input[i] == '-') outString.Append("m");
                }
                else if ("(*/%-+".Contains(input[i]))
                {
                    nextpmIsUnar = true;
                    outString.Append(input[i]);
                }
                else
                {
                    nextpmIsUnar = false;
                    outString.Append(input[i]);
                }
            }
            return outString.ToString();
        }

        List<string> tokenizing(string input)
        {
            List<string> tokens = new List<string>();
            StringBuilder number = new StringBuilder();
            bool decimalSeparatorFlag = false;
            for (int i = 0; i < input.Length; i++)
            {
                if ('0' <= input[i] && input[i] <= '9')
                {
                    number.Append(input[i]);
                }
                else if (input[i] == '.' || input[i] == ',')
                {
                    if (decimalSeparatorFlag)
                        throw new Exception($"Два десятичных разделителя в числе '{number}' в позиции {i}");
                    decimalSeparatorFlag = true;
                    number.Append(',');
                }
                else
                {
                    if (number.Length > 0)
                    {
                        tokens.Add(number.ToString());
                        decimalSeparatorFlag = false;
                        number.Clear();
                    }
                    if ("*/%+-()!pm".Contains(input[i]))
                        tokens.Add(input[i].ToString());
                    else
                        throw new Exception($"Недопустимый символ '{input[i]}' в позиции {i}");
                }
            }
            if (number.Length > 0)
            {
                tokens.Add(number.ToString());
                decimalSeparatorFlag = false;
                number.Clear();
            }

            return tokens;
        }

        List<string> shuntingYard(List<string> tokens)
        {
            List<string> rpn = new List<string>();
            Stack<string> stack = new Stack<string>();
            double number;

            foreach (string token in tokens)
            {
                if (Double.TryParse(token, out number))
                {
                    rpn.Add(token); // число
                }
                else if ("!".Contains(token))
                {
                    rpn.Add(token); // постфиксные функции
                }
                else if ("pm".Contains(token))
                {
                    stack.Push(token); // добавление префикных функций в стек
                }
                else if (token == "(")
                {
                    stack.Push("(");
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        rpn.Add(stack.Pop()); // извлечение всего, что между скобками
                    }
                    if (stack.Count == 0)
                        throw new Exception("Некорректная расстановка скобок");
                    stack.Pop();
                }
                else if ("*/%+-".Contains(token))
                {
                    while (stack.Count > 0 &&
                        (
                        "pm".Contains(stack.Peek()) || // префикные операции
                        "*/%".Contains(stack.Peek()) || // операции высокого приоритета
                        ("+-".Contains(stack.Peek()) && "+-".Contains(token)) // операций такого же приоритета
                        ))
                    {
                        rpn.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
            }

            while (stack.Count > 0)
            {
                if ("*/%+-!pm".Contains(stack.Peek()) == false)
                    throw new Exception("Некорректная расстановка операторов");
                rpn.Add(stack.Pop());
            }

            return rpn;
        }

        long factorial(long a)
        {
            long fact = 1;
            for (long i = 2; i <= a; i++)
            {
                fact *= i;
            }
            return fact;
        }
    }
}
