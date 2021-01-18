using System;
using System.Collections.Generic;
using System.Text;

namespace MGOAP_Test.Conditions {
    class AxeAvailable : MGOAP.Condition {
        public override bool Evaluate() {
            if (WorldState.Axes.Count > 0)
                return true;
            else
                return false;
        }
    }
}
