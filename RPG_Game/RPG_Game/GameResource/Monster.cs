using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.GameResource
{
    class Monster 
    {
        public Monster Clone()
        {
            Monster mob = new Monster();
            return mob;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Gold { get; set; }
    }
}
