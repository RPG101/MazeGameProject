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
        //sets a value to store the current randomly selected room
        public static int[,] CurrentRoom;
        static bool RandomStartComplete = false;
        // defines the max rooms to be used when rooms folder is loaded
        static int MaxRooms = 0;
        // initialise dictionary to store room data.
        public static Dictionary<int, int[,]> RoomDict = new Dictionary<int, int[,]>();
        public static List<int[,]> MazeData = new List<int[,]>();
        
        public static Dictionary<int, int[,]> LoadRooms()
        {
            //gets files from the given folder directory
            string[] RoomPaths = Directory.GetFiles(@"..\..\..\Levels\");
            // iterate over each room string 
            foreach(string room in RoomPaths)
            {

                if (File.Exists(room))
                {
                // creates a string to store all text from file.
                string lines = File.ReadAllText(room);

                // creates storage for each x and y value
                int x = 0, y = 0;
                int[,] RoomData = new int[9,9];
                // loops over each line in the file
                foreach (var row in lines.Split('\n'))
                {
                    // sets the y value as 0 for a start point each loop
                    y = 0;
                    // takes the col and rows in the file and splits on each space
                    foreach (var col in row.Trim().Split(' '))
                    {
                        // from each x and y value parse and store into array
                        RoomData[x, y] = int.Parse(col.Trim());
                        // update y value
                            y++;
                    }
                    // update x value
                    x++;
                }
                    RoomDict.Add(MaxRooms, RoomData);
                    MaxRooms++;

                }

            }
            CurrentRoom = RoomDict[0];
            return RoomDict;
        }

        public static int[,] RandomRoom()
        {
            
            if (RandomStartComplete == true)
            {
                return CurrentRoom;
            }
            //creates a new random to be used for generating random rooms.
            Random MapStart = new Random();
            //creates a vairable to store the random interger, will select rooms 0 - 6 as room 7 should be room with an exit, a player should not spawn into this room.
            var RandomMap = MapStart.Next(0, 6);
            //checks to see if the room dictonary has a key that corrosponds to the random interger.
            if(RoomDict.ContainsKey(RandomMap))
            {
                //sets the string of the room key to the string of a room file, to be used later.
                CurrentRoom = RoomDict[RandomMap];
            } else
            {
                // if the key value in the dictonary does not exist then throw an error and exit application.
                switch (MessageBox.Show(GameForm.ActiveForm, "Map Room does not exist.", "Closing", MessageBoxButtons.OK))
                {
                    case DialogResult.OK:
                        Application.Exit();
                        break;
                }
            }
            RandomStartComplete = true;
            // returns the string for the map file location.
            return CurrentRoom;
            
        }
    }
}
