using System;
using System.Collections.Generic;
using System.Text;
using MGOAP;
using MGOAP_Test.Goals;

namespace MGOAP_Test.Motivators {
    class ElfMotivator : MGOAP.Motivator {
        Goal gatherWood = new GatherWood(65);

        public override Goal GetGoal() {
            return gatherWood;
        }

        public override int GetPriority() {
            return 100;
        }
    }
}
