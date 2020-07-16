using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int f;
            int s;
            int r;
            Console.WriteLine("Hello World!");
            Console.WriteLine(3 + 5);
            f = Int32.Parse(Console.ReadLine());
            s= Int32.Parse(Console.ReadLine());
            r = f + s;
            Console.Write(r);
        }
    }
}
