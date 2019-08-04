using System;
using System.Collections.Generic;
using System.Text;
using MGOAP;

namespace MGOAP_Test.Actions
{
    internal class FellTree : MGOAP.Action
    {
        public FellTree()
        {

        }

        public override int GetCost() => 15;
        public override bool InRange() => true;

        public override void PreformAction()
        {
            Console.WriteLine("Tree Felled!");
        }
    }
}
