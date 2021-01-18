using System;
using System.Collections.Generic;
using System.Text;
using MGOAP;

namespace MGOAP_Test.Actions {
    internal class PickupAxe : MGOAP.Action {

        public PickupAxe(Elf owner) : base(null) {

        }

        public override int GetCost() => 1;
        public override bool InRange() => true;

        public override void PerformAction() {
            Console.WriteLine("Tree Felled!");
        }
    }
}
