using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fordito
{
    public static class Program
    {
        public static string[,] matrix = new string[12, 7];
        public static string[] sArray = new string[7];
        public static List<string> sList = new List<string>();

        public static Stack stack;

        public static bool format(string matrixElem)
        {
            if (matrixElem.Length == 0)
            {
                Console.WriteLine("Nem megálló állapot");
                check = true;
                return true;
            }

            if (matrixElem.Trim() == "elfogad")
            {
                Console.WriteLine("Elfogad");
                check = true;
                return true;
            }

            if (matrixElem.Trim() == "pop")
            {
                //helper = stack.Pop().ToString();
                input = input.Substring(1);
                return false;
            }


            if (matrixElem.Contains('('))
            {
                string seged = matrixElem.Substring(1).Split(',')[0];

                for (int j = seged.Length - 1; j >= 0; j--)
                {
                    if (seged[j].Equals('e'))
                    {
                        continue;
                    }
                    stack.Push(seged[j].ToString());
                }
            }

            if (matrixElem.Contains(')'))
            {
                ruleNumber += matrixElem.Substring(0, matrixElem.Length - 1).Split(',')[1];
            }
            //Console.WriteLine("input: {0},  {1},    {2}", input,stackContain,ruleNumber);
            string stackContain = "";
            foreach (string element in stack)
            {
                stackContain += element;
            }

            sw.WriteLine("{0}, {1}, {2}", input, stackContain, ruleNumber);
            sw.Flush();

            return false;
        }



        public static void fillTheMatrix()
        {
            StreamReader sr = new StreamReader("rule.txt");
            string helper = "";
            int i = 0;
            while (!sr.EndOfStream)
            {
                helper += sr.ReadLine();
                for (int k = 0; k < sArray.Length; k++)
                {
                    sArray[k] = helper.Split('|')[k];
                }
                for (int j = 0; j < sArray.Length; j++)
                {
                    matrix[i, j] = sArray[j];

                }
                helper = "";
                i++;
            }
        }

        public static StringBuilder sb = new StringBuilder();
        public static string ruleNumber = "";
        public static string input = "(203+304)*55#";
        public static bool check = false;
        public static StreamWriter sw = new StreamWriter("output.txt");
        public static string helper;

        static void Main(string[] args)
        {
            fillTheMatrix();
            //string helper;
            string[] elements = new string[2];
            input = simple(input);
            stack = new Stack();
            stack.Push("#");
            stack.Push("E");
            if (checkInput(input, matrix))
            {
                do
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (input[0].ToString() == matrix[0, j])
                        {
                            helper = stack.Pop().ToString();
                            if (helper == "#" && input == "#")
                            {
                                sw.WriteLine("Elfogad");
                                sw.Flush();
                            }
                            for (int t = 0; t < matrix.GetLength(0); t++)
                            {
                                if (helper == matrix[t, 0])
                                {
                                    format(matrix[t, j]);
                                }
                            }
                        }
                    }
                } while (!check);
            }
            else
            {
                Console.WriteLine("Nem megfelelő input");
                sw.WriteLine("Nem megfelelő input");
                sw.Flush();
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(ruleNumber);
            string notepadPath = Environment.SystemDirectory + "\\notepad.exe";

            var startInfo = new ProcessStartInfo(notepadPath)
            {
                Arguments = "output.txt"
            };
            Process.Start(startInfo);
            sw.Close();
            Console.ReadLine();

        }

        public static bool checkInput(string input, string[,] matrix)
        {
            string characters = "";
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                characters += matrix[0, j];
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (!characters.Contains(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static string simple(string st)
        {
            return Regex.Replace(st, @"([0-9]+)", "i");
        }


    }
}
