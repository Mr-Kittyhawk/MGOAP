using System;
using System.Collections.Generic;
using System.Text;

namespace MGOAP_Test
{
    internal static class WorldState
    {
        //environment
        internal static List<Tree> Trees = new List<Tree>(10);

        public static void Print()
        {
            Console.WriteLine("---------- World ----------");
            Console.WriteLine("Trees: " + Trees.Count);
            Console.WriteLine();
        }
    }
}
