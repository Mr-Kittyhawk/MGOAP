using System;
using System.Collections.Generic;
using System.Text;

namespace MGOAP
{
    static class ArrayExtensionMethods
    {
        public static bool Contains(this Condition[] array, Condition thing)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(thing))
                    return true;
            }
            return false;
        }
    }
}
