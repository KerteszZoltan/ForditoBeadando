using System;   
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fordito
{
    public static class Program
    {
        //public static string[][] matrix = new string[6][];
        public static string[,] matrix = new string[12, 7];
        public static string[] sArray = new string[7];
        public static List<string> sList = new List<string>();

        public static Stack stack;

        public static bool format(string matrixElem)
        {
            
            if (matrixElem.Length == 0)
            {
                Console.WriteLine("0");
                check = true;
                return true;
            }

            if(matrixElem.Trim() == "elfogad")
            {
                Console.WriteLine("Elfogad"); 
                check = true;
                return true;
            }

            if(matrixElem.Trim() == "pop")
            {
                //stack.Pop();
                input = input.Substring(1); //+1 vagy -1
                //input = input.Substring(0) + input.Substring(i+1,input.Length); //+1 vagy -1
                return false;
            }

            //string[] result = new string[2];

            if (matrixElem.Contains('('))
            {
                string seged = matrixElem.Substring(1).Split(',')[0];
                for (int j = seged.Length-1; j >= 0; j--)
                {
                    stack.Push(seged[j].ToString());
                }
            }

            if (matrixElem.Contains(')'))
            {
                ruleNumber += matrixElem.Substring(0, matrixElem.Length - 1).Split(',')[1];
            }  
            
            /*
            stack.Push(matrixElem.Trim().Split(',')[0]);
            sb.Append(matrixElem.Trim().Split(',')[1]);
            ruleNumber = sb.ToString();*/
            
            return false;
        }

        /*
        private void TrimBrackets(ref string element)
        {
            element = element.Substring(element.IndexOf('(') + 1);
            element = element.Substring(0, element.IndexOf(')'));
        }*/


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
                //sArray = helper.Split('|');
                //sList.AddRange(sr.ReadLine().Split('|').ToList());
                for (int j = 0; j < sArray.Length; j++)
                {
                    //matrix[i][j] = sList[j];
                    matrix[i, j] = sArray[j];

                }
                helper = "";
                i++;
            }
        }

        public static StringBuilder sb = new StringBuilder();
        public static string ruleNumber = "";
        public static string input = "(i+i)*i#";
        public static bool check = false;

        static void Main(string[] args)
        {
            fillTheMatrix();
            string helper;
            string[] elements = new string[2];
            
            stack = new Stack();
            stack.Push("#");
            stack.Push("E");
            
            do
            {
                //for (int i = 0; i < input.Length; i++)
                //{
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (input[0].ToString() == matrix[0, j])
                    {
                        helper = stack.Pop().ToString();
                        for (int t = 0; t < matrix.GetLength(0); t++)
                        {
                            if (helper == matrix[t, 0])
                            {
                                ///check = format(matrix[t, j]);
                                format(matrix[t, j]);
                                

                            }
                        }
                    }
                }
                //}
            }while(!check); /*while (input != "#" && stack.Count >= 0);*/


            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(ruleNumber);
            Console.ReadLine();
        }
    }
}
