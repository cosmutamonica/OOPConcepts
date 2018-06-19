using System;
using System.Collections.Generic;
using System.Text;

namespace OOPConcepts
{
    interface Interface
    {
         void M1();
    }
    interface I
    {
        void M();
    }
    abstract class C : I
    {
        //cannot declare a body because it is marked abstract 
        public abstract void M();

    }
    class R : I,Interface
    {
        public void M()
        {
            //must declare a body
        }

        public void M1()
        {
            throw new NotImplementedException();
        }
    }
    class R1 : C
    {
        public override void M()
        {
            throw new NotImplementedException();
        }
    }
}
