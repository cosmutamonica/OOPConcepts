using System;
using System.Collections.Generic;
using System.Text;

namespace OOPConcepts
{
    // this is a static class as can't be instantiated. You access the members of a static class by using the class name itself.
    //Contains only static members.
    //    Cannot be instantiated.
    //    Is sealed.
    static class StaticClass
    {
        public static void Foo()
        {
            Console.WriteLine("Static class");
        }
    }
}
