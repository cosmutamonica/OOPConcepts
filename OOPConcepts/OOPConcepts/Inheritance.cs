using System;
using System.Collections.Generic;
using System.Text;

namespace OOPConcepts
{
    class Inheritance
    {
    }
    class Parent
    {
        public void Foo(int x)
        {
            Console.WriteLine("Parent.Foo(int x)");
        }
    }

    class Child : Parent
    {
        public int MyProperty { get; set;}
        public void Foo(double y)
        {
            Console.WriteLine("Child.Foo(double y)");
        }
    }
}
