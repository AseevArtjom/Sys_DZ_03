using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Sys_DZ_03
{
    internal class Program
    {
        /*
        Написать программу, которая выполняется в два потока.
        В первом потоке считывается содержимое текстового файла (последовательность чисел, количество чисел от 1000 и более),
        в другом потоке происходит вывод чисел по возрастанию и вывод их на экран
        */
        static List<int> numbers = new List<int>();

        static void Main(string[] args)
        {
            Thread ReaderThread = new Thread(ReadNumbers);
            ReaderThread.Start();
            ReaderThread.Join();


            Thread writerThread = new Thread(WriteNumbers);
            writerThread.Start();

            Console.ReadKey();
        }



        static void ReadNumbers()
        {
            string path = "numbers.txt";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string ln;
                    while ((ln = reader.ReadLine()) != null)
                    {
                        if (int.TryParse(ln, out int number))
                        {
                            numbers.Add(number);
                            Thread.Sleep(10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        static void WriteNumbers()
        {
            while (numbers.Count > 0)
            {
                numbers.Sort();
                Console.WriteLine("Sort : ");
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
                numbers.Clear();
            }
        }
    }
}
