using System;
using System.Collections.Generic;
using System.Text;

namespace MGOAP_Test.Conditions
{
    class HasAxe: MGOAP.Condition
    {
        private List<Item> inventory;
        public HasAxe(Elf owner)
        {
            inventory = owner.Inventory;
        }

        public override bool Evaluate()
        {
            for(int i =0; i< inventory.Count; i++)
            {
                if (inventory[i].GetType() == typeof(Axe))
                    return true;
            }
            return false;
        }
    }
}
