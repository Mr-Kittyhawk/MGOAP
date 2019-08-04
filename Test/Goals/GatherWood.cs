using System;
using System.Collections.Generic;
using System.Text;
using MGOAP;
using MGOAP_Test.Conditions;

namespace MGOAP_Test.Goals
{
    public class GatherWood : MGOAP.Goal
    {
        public GatherWood(int howMuchWood) : base(new EnoughWood(howMuchWood))
        {
            
        }
    }
}
