using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGame
{
    public class Room
    {
        //sets the room constructor to take the map from the uploaded file.
        public Room(int[,] map)
        {
            Map = map;
        }

        // bools to check if threats or coins exist in room
        public bool NoThreats;
        public bool NoCoins;
        public bool HasEntered;
        public bool HasExit;

        //map data storage
        public int[,] Map;

        //directions used for passages
        public int North;
        public int East;
        public int south;
        public int West;
    }
}
