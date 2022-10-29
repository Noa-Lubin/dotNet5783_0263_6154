// See https://aka.ms/new-console-template for more information
using System;
namespace targil0
{
    partial class Program
    {
        static void main()
        {
            Welcome0263();
            Welcome6154();
            Console.ReadKey();
        }
        static partial void Welcome6154();

        private static void Welcome0263()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}

