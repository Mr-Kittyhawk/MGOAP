using System;
using System.Collections.Generic;
using System.Text;
using MGOAP;

namespace MGOAP_Test.Actions
{
    internal class PickupAxe : MGOAP.Action
    {
        public void new

        public override int GetCost() => 1;
        public override bool InRange() => true;

        public override void PreformAction()
        {
            Console.WriteLine("Tree Felled!");
        }
    }
}
