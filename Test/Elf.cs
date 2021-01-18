using System;
using System.Collections.Generic;
using System.Text;
using MGOAP_Test.Motivators;
using MGOAP_Test.Actions;
using MGOAP;

namespace MGOAP_Test {
    class Elf {
        public string Name;
        public List<Item> Inventory;
        private MGOAP.Agent ai;

        public Elf(string name) {
            Name = name;

            var actions = new List<MGOAP.Action> {
                new FellTree(this),
                new PickupAxe(this)
            };

            var motivations = new List<MGOAP.Motivator> {
                new ElfMotivator()
            };

            ai = new MGOAP.Agent(actions, motivations);

        }
    }
}
