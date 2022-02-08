using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.GameResource
{
    class Hero
    {
        public int ID { get; set; }
        public string Class { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public string Skill { get; set; }
        public string Tips { get; set; }
        public double Percent { get; set; }
    }
}
