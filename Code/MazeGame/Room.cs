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
        int MaxRooms = 0;
        Dictionary<int, string> RoomDict = new Dictionary<int, string>();
        public Dictionary<int,string> LoadRooms()
        {
            

            string[] RoomPaths = Directory.GetFiles(@"..\..\..\Levels\");
            foreach(string room in RoomPaths)
            {
                
                RoomDict.Add(MaxRooms, room);
                MaxRooms++;


            }
            return RoomDict;
        }

        public string RandomRoom()
        {
            string value = " ";
            Random MapStart = new Random();
            var RandomMap = MapStart.Next(0, 8);
            if(RoomDict.ContainsKey(RandomMap))
            {
                value = RoomDict[RandomMap];
            } else
            {
                switch (MessageBox.Show(GameForm.ActiveForm, "Map Room", "Closing", MessageBoxButtons.OK))
                {
                    case DialogResult.OK:
                        Application.Exit();
                        break;
                }
            }
            return value;
        }

        


    }
}
