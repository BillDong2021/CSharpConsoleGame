using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.GameResource
{
    class Map
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int[] MonsterLocation { get; set; }
        public int[] TreasureLocation { get; set; }
    }
}
