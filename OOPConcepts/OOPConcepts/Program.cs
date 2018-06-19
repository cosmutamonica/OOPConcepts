using System;
using System.Net.Http;

namespace OOPConcepts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //base class
            Employee E = new Employee();
            E.GetInfo();
            //inheritance
            Child c = new Child();
            c.Foo(10);//child foo
            Parent c2 = new Child();
            c2.Foo(10);//parent foo
            c.MyProperty = 5;//property is affected
            ChangeValue(c);
            ChangeValueToNull(c);//ref object is not affected, c is not null

            int x = 1;
            ChangeValue(x);
            //overloading 
            Foo(5, 10);

            //static class
            StaticClass.Foo();

            // 
            Singleton s1 = Singleton.Instance;
            Singleton s2 = Singleton.Instance;
            if (s1 == s2)
            {
                Console.WriteLine("The singleton implementation works!");
            }
            else
            {
                Console.WriteLine("Wrong implementation");
            }


        }

        //overloading
        static void Foo(int x, int y)
        {
            Console.WriteLine("Foo1(int x, int y)");
        }

        static void Foo(int x, double y)
        {
            Console.WriteLine("Foo1(int x, double y)");
        }

        static void Foo(double x, int y)
        {
            Console.WriteLine("Foo3(double x, int y)");
        }

        static void ChangeValueToNull(Child c)
        {
            //this will not cause changes
            c = null;
        }

        static void ChangeValue(Child c)
        {
            //the value will be changed 
            c.MyProperty =6;
        }

        static void ChangeValue(int i)
        {
            //the value will be changed 
            i = 5;
        }


    }
}
