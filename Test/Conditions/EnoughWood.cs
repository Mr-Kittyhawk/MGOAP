using System;
using System.Collections.Generic;
using System.Text;

namespace MGOAP_Test.Conditions {
    public class EnoughWood : MGOAP.Condition {
        private int enoughWood;

        public EnoughWood(int howMuch) {
            enoughWood = howMuch;
        }

        public override bool Evaluate() {
            if (Stockpile.Wood > enoughWood)
                return true;
            else
                return false;
        }
    }
}
