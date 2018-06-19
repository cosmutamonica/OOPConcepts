using System;
using System.Collections.Generic;
using System.Text;

namespace OOPConcepts
{
    //An abstract class cannot be instantiated. 
    //The purpose of an abstract class is to provide a common definition of a base class that multiple derived classes can share. 
    abstract class AbstractClass
    {
        public abstract void DoWork(int i);
    }
    public class D
    {
        public virtual void DoWork(int i)
        {
            // Original implementation.
        }
    }
    public class A : D
    {
        public override void DoWork(int i)
        {
            Console.WriteLine("Overriding a virtual method");
        }
    }

    public abstract class E : D
    {
        public abstract override void DoWork(int i);
    }

    public class F : E
    {
        public override void DoWork(int i)
        {
            // New implementation.
        }
    }
}
