using System;
using System.Collections.Generic;
using System.Text;

namespace OOPConcepts
{
    //The class is marked sealed to prevent derivation, which could add instances.
    sealed class Singleton
    {
        //The public static property provides a global access point to the instance. 
        private static readonly Singleton instance = new Singleton();

        //because the constructor is private, the Singleton class cannot be instantiated outside of the class itself
        private Singleton()
        {

        }

        //readonly property
        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }
    }

    //best way to implement singleton
    public sealed class MultithreadedSingleton
    {
        //variable is declared to be volatile to ensure that assignment to the instance variable completes before the instance variable can be accessed
        private static volatile MultithreadedSingleton instance;
        //used fo Multithreaded
        private static object syncRoot = new Object();

        private MultithreadedSingleton() { }

        public static MultithreadedSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    //used fo Multithreaded
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MultithreadedSingleton();
                    }
                }

                return instance;
            }
        }
    }
}
