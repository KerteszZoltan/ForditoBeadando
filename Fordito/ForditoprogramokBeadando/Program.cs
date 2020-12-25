using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace fordito
{
    public static class Program
    {
        public static string[,] matrix = new string[12, 7];
        public static string[] tomb = new string[7];
        public static List<string> lista = new List<string>();
        public static Stack verem;
        public static StringBuilder sb = new StringBuilder();
        public static string seged;

        public static string ellenorzo = "";
        public static string input = "(203+304)*55#";
        public static bool ellenor = false;
        public static StreamWriter sw = new StreamWriter("output.txt");

        static void Main(string[] args)
        {
            MatrixFeltoltes();
            string[] elemek = new string[2];
            input = simple(input);
            verem = new Stack();
            verem.Push("#");
            verem.Push("E");
            if (InputEllen(input, matrix))
            {
                do
                {
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        if (input[0].ToString() == matrix[0, i])
                        {
                            seged = verem.Pop().ToString();
                            if (seged == "#" && input == "#")
                            {
                                sw.WriteLine("Elfogad");
                                sw.Flush();
                            }
                            for (int j = 0; j < matrix.GetLength(0); j++)
                            {
                                if (seged == matrix[j, 0])
                                {
                                    Format(matrix[j, i]);
                                }
                            }
                        }
                    }
                } while (!ellenor);
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
            Console.WriteLine(ellenorzo);
            string notepad = Environment.SystemDirectory + "\\notepad.exe";

            var startInfo = new ProcessStartInfo(notepad)
            {
                Arguments = "output.txt"
            };
            Process.Start(startInfo);
            sw.Close();
            Console.ReadLine();
        }
        public static bool Format(string matrixElem)
        {
            if (matrixElem.Length == 0)
            {
                Console.WriteLine("Nem megálló állapot");
                ellenor = true;
                return true;
            }

            if (matrixElem.Trim() == "elfogad")
            {
                Console.WriteLine("Elfogad");
                ellenor = true;
                return true;
            }

            if (matrixElem.Trim() == "pop")
            {
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
                    verem.Push(seged[j].ToString());
                }
            }

            if (matrixElem.Contains(')'))
            {
                ellenorzo += matrixElem.Substring(0, matrixElem.Length - 1).Split(',')[1];
            }
            //Console.WriteLine("input: {0},  {1},    {2}", input,stackContain,ruleNumber);
            string veremMely = "";
            foreach (string elem in verem)
            {
                veremMely += elem;
            }

            sw.WriteLine("{0}, {1}, {2}", input, veremMely, ellenorzo);
            sw.Flush();

            return false;
        }
        public static void MatrixFeltoltes()
        {
            StreamReader sr = new StreamReader("rule.txt");
            string seged = "";
            int i = 0;
            while (!sr.EndOfStream)
            {
                seged += sr.ReadLine();
                for (int j = 0; j < tomb.Length; j++)
                {
                    tomb[j] = seged.Split('|')[j];
                }
                for (int k = 0; k < tomb.Length; k++)
                {
                    matrix[i, k] = tomb[k];

                }
                seged = "";
                i++;
            }
        }
        public static bool InputEllen(string input, string[,] matrix)
        {
            string characters = "";
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                characters += matrix[0, i];
            }
            for (int j = 0; j < input.Length; j++)
            {
                if (!characters.Contains(input[j]))
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
