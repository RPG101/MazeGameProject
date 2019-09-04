using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MazeGame
{
    class Passage
    {
        public Passage(int CurrentRoom, int NextRoom, PassageDirection RoomLink, int x, int y, bool PlayerEntered) { 
}

        public struct Passages
        {
            public int CurrentRoom;
            public int NextRoom;
            public Passage.PassageDirection RoomLink;
            public int x;
            public int y;
            public bool PlayerEntered;
        }

        public List<string> PassageLink = new List<string>();

        public enum PassageDirection
        {
            North,
            East,
            South,
            West,
        };

        public void LoadPassageConfig()
        {
            string PassageConfigFile = @"..\..\..\Config\Config.txt";
            // checks if the file exists.
            if (File.Exists(PassageConfigFile))
            {

                string lines = File.ReadAllText(PassageConfigFile);

                foreach (string row in lines.Split('\n'))
                {
                    foreach (var col in row.Trim().Split(' '))
                    {
                        PassageLink.Add(col.Trim());
                    }

                }

            }
        }

    } 
}
