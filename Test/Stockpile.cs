using System;
using System.Collections.Generic;
using System.Text;

namespace MGOAP_Test {
    public static class Stockpile {
        internal static int Wood;
        internal static List<Axe> Axes = new List<Axe>(5);

        public static void Print() {
            Console.WriteLine("-------- Stockpile --------");
            Console.WriteLine("Wood: " + Wood);
            Console.WriteLine("Axes: " + Axes);
        }
    }
}
