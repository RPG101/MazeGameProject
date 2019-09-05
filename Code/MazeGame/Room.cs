using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGame
{
    class Room
    {
        public Room(int[,] map)
        {
            Map = map;
        }

        public bool NoThreats;
        public bool NoCoins;

        public int[,] Map;

        public int North;
        public int East;
        public int south;
        public int West;

        public Dictionary<int, Room> RoomDict = new Dictionary<int, Room>();


    }
}
