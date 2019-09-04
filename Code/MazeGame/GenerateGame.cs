using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace MazeGame
{
    internal class GenerateGame
    {
        // enum for player movement to set each direction.
        public enum Movement
        {
            None,
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        // A struct to store the player object information such as x, y values and the current movement value
        public struct Player
        {
            public int x;
            public int y;
            public Movement PlayerDirection;
        }

        // A struct to store the Threats information, its x and y values and its current movement value
        public struct Threat
        {
            public int x;
            public int y;
            public Movement ThreatDirection;
        }

        public struct Passages
        {
            public int[,] CurrentRoom;
            public string NextRoom;
            public Passage.PassageDirection RoomLink;
            public int x;
            public int y;
            public bool PlayerEntered;
        }



        // data for each block default WxH
        private const int CurrentWidth = 30;
        private const int CurrentHeight = 30;
        // creates a block array to store each block for the picture box locaiton.
        private IBlock[,] GameRegions;
        //creates a picturebox to add graphics to the display, this will be updated into the main form.
        private PictureBox MazeDisplayBox;
        //creates a new room object to allow access to functions for random rooms and loading of rooms.
        private Passages[] PassagesInfo = new Passages[9];
        //player movement to set current direction based on key pressed.
        public Movement PlayerMovement { set { Player1.PlayerDirection = value; } }
        // creates a player object to store current x and y values + movement.
        public Player Player1;
        // creates a new player object to store the updated x and y values when movement occurs. 
        public Player Updatedplayer;
        //boolean to set the game win condition.
        public bool GameWin { get; private set; } = false;

        public int[,] LoadMap(string lInput)
        {
            // creates a 9x9 2d array to store map x and y values (should always be 9x9)
            int[,] MapLoadArray = new int[9, 9];

            // takes the string from input.
            string textFile = lInput;
            // checks if the file exists.
            if (File.Exists(textFile))
            {
                // creates a string to store all text from file.
                string lines = File.ReadAllText(textFile);

                // creates storage for each x and y value
                int x = 0, y = 0;

                // loops over each line in the file
                foreach (var row in lines.Split('\n'))
                {
                    // sets the y value as 0 for a start point each loop
                    y = 0;
                    // takes the col and rows in the file and splits on each space
                    foreach (var col in row.Trim().Split(' '))
                    {
                        // from each x and y value parse and store into array
                        MapLoadArray[x, y] = int.Parse(col.Trim());
                        // update y value
                        y++;
                    }
                    // update x value
                    x++;
                }
            }
            //store the values for the map into the game region to be replaced by a block.
            GameRegions = new IBlock[MapLoadArray.GetLength(0), MapLoadArray.GetLength(1)];
            return MapLoadArray;
        }

        public void CreateMap()
        {
            int PassageArrayNum = 0;
            //loads a random room from the room dictonary
            int[,] LoadedMapArray = Room.RandomRoom();

            //loops over the x and y values loaded in the map
            for (int CurrentX = 0; CurrentX < LoadedMapArray.GetLength(0); CurrentX++)
            {
                for (int CurrentY = 0; CurrentY < LoadedMapArray.GetLength(1); CurrentY++)
                {
                    // if the file contains a 0 set this as a background block
                    if (LoadedMapArray[CurrentX, CurrentY] == 0)
                    {
                        GameRegions[CurrentX, CurrentY] = new BackgroundBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                    }
                    // if the file contains a 1 set this as a wall block.
                    if (LoadedMapArray[CurrentX, CurrentY] == 1)
                    {
                        GameRegions[CurrentX, CurrentY] = new WallBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                    }
                    //if the file contains a 2 set this as a passage block
                    if (LoadedMapArray[CurrentX, CurrentY] == 2)
                    {
                        GameRegions[CurrentX, CurrentY] = new PassageBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                        PassagesInfo[PassageArrayNum].x = CurrentX;
                        PassagesInfo[PassageArrayNum].y = CurrentY;
                        PassagesInfo[PassageArrayNum].CurrentRoom = Room.CurrentRoom;
                        PassageArrayNum++;
                    }
                    //if the file contains a 3 set this as an exit block.
                    if (LoadedMapArray[CurrentX, CurrentY] == 3)
                    {
                        GameRegions[CurrentX, CurrentY] = new ExitBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                        ExitBlock.CurrentX = CurrentX;
                        ExitBlock.CurrentY = CurrentY;
                    }
                }
            }
            // renders the map once all data has been loaded and set to a specific block
            Rendermap();
        }

        public GenerateGame(PictureBox GridGen)
        {
            //start up constructor to create the maze and its grid size for a room.
            this.MazeDisplayBox = GridGen;

            int GridpointX = 9;
            int GridpointY = 9;

            //Creates all grid points as a block
            GameRegions = new IBlock[GridpointX, GridpointY];

            //sets a random location for the player
            Random RandLocation = new Random();
            Player1.x = RandLocation.Next(1, 6);
            Player1.y = RandLocation.Next(1, 6);

            //loads all room configuration files.
            Room.LoadRooms();
            //Passage.LoadPassageConfig();

        }

        public void Rendermap()
        {
            //sets the map as the size of the picture box
            Bitmap MapImage = new Bitmap(MazeDisplayBox.Width, MazeDisplayBox.Height);

            //creates a graphic handle from the picture box
            Graphics MapGraphic = Graphics.FromImage(MapImage);

            //sets the quality of image and interpolation
            MapGraphic.InterpolationMode = InterpolationMode.Low;
            MapGraphic.CompositingQuality = CompositingQuality.HighSpeed;

            //creates all blocks that are stored in the game regions, this can be the Background block(just border lines), wallblock and passages/exit.
            foreach (IBlock Map in GameRegions)
            {
                Map.RenderBlock(MapGraphic);
            }

            //renders the player to the graphic.
            SolidBrush fill = new SolidBrush(Color.Blue);
            MapGraphic.FillRectangle(fill, new Rectangle(Player1.x * CurrentWidth, Player1.y * CurrentHeight, CurrentWidth, CurrentHeight));

            //sets the current display box in the form to the picturebox declared above.
            MazeDisplayBox.Image = MapImage;
            //refreshes the data for the map.
            MazeDisplayBox.Refresh();
        }

        public Player UpdateMove(Player lplayer)
        {
            //Holds current player location in a new vairable
            Updatedplayer = lplayer;

            //updates the new player information
            switch (Updatedplayer.PlayerDirection)
            {
                case Movement.UP:
                    Updatedplayer.y -= 1;
                    break;

                case Movement.DOWN:
                    Updatedplayer.y += 1;
                    break;

                case Movement.RIGHT:
                    Updatedplayer.x += 1;
                    break;

                case Movement.LEFT:
                    Updatedplayer.x -= 1;
                    break;

                default:
                    break;
            }
            //provents movement through walls, this is done if the updated player location is a wall then just return the current player without changes
            if (GameRegions[Updatedplayer.x, Updatedplayer.y].SolidBlock == true) return lplayer;

            // if the location is not a wall from previous statment then the player location is updated correctly
            return Player1 = Updatedplayer;
        }

        public void UpdateGame()
        {
            //calls the code to update a player's move
            UpdateMove(Player1);
            //renders the map if changes have occured e.g. threat defeated, coin obtained or player moved. all need to be re-displayed on the screen
            LinkRooms();
            Rendermap();
            //sets the win condition for the exit block.
            
            GameWin = Player1.x == ExitBlock.CurrentX && Player1.y == ExitBlock.CurrentY;
            
        }

        public void LinkRooms()
        {
            if (Room.CurrentRoom == Room.RoomDict[0])
            {
                bool EnteredNorth = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredWest = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;

                if (EnteredNorth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[1];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 6;

                }

                if (EnteredWest == true)
                {
                    Room.CurrentRoom = Room.RoomDict[5];
                    CreateMap();
                    Player1.x = 7;
                    Player1.y = 3;
                }

            }

            if (Room.CurrentRoom == Room.RoomDict[1])
            {
                bool EnteredNorth = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredWest = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;
                bool EnteredSouth = Player1.x == PassagesInfo[2].x && Player1.y == PassagesInfo[2].y;

                if (EnteredSouth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[0];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 1;
                }

                if(EnteredWest == true)
                {
                    Room.CurrentRoom = Room.RoomDict[3];
                    CreateMap();
                    Player1.x = 7;
                    Player1.y = 3;
                }

                if(EnteredNorth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[2];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 6;
                }

            }

            if (Room.CurrentRoom == Room.RoomDict[2])
            {
                bool EnteredSouth = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredWest = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;

                if(EnteredSouth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[1];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 1;

                }

                if(EnteredWest == true)
                {
                    Room.CurrentRoom = Room.RoomDict[7];
                    CreateMap();
                    Player1.x = 7;
                    Player1.y = 3;
                }

            }


            if (Room.CurrentRoom == Room.RoomDict[3])
            {
                bool EnteredSouth = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredWest = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;
                bool EnteredEast = Player1.x == PassagesInfo[2].x && Player1.y == PassagesInfo[2].y;

                if(EnteredSouth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[5];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 1;

                }

                if(EnteredEast == true)
                {
                    Room.CurrentRoom = Room.RoomDict[1];
                    CreateMap();
                    Player1.x = 1;
                    Player1.y = 3;

                }

                if(EnteredWest == true)
                {
                    Room.CurrentRoom = Room.RoomDict[4];
                    CreateMap();
                    Player1.x = 7;
                    Player1.y = 3;
                }


            }

            if (Room.CurrentRoom == Room.RoomDict[4])
            {
                bool EnteredEast = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredNorth = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;

                if (EnteredEast == true)
                {
                    Room.CurrentRoom = Room.RoomDict[3];
                    CreateMap();
                    Player1.x = 1;
                    Player1.y = 3;

                }

                if(EnteredNorth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[6];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 6;
                }

            }

            if (Room.CurrentRoom == Room.RoomDict[5])
            {
                bool EnteredEast = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredNorth = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;

                if (EnteredNorth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[3];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 6;

                }

                if (EnteredEast == true)
                {
                    Room.CurrentRoom = Room.RoomDict[0];
                    CreateMap();
                    Player1.x = 1;
                    Player1.y = 3;
                }
            }

            if(Room.CurrentRoom == Room.RoomDict[6])
            {
                bool EnteredSouth = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;
                bool EnteredEast = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;

                if(EnteredSouth == true)
                {
                    Room.CurrentRoom = Room.RoomDict[4];
                    CreateMap();
                    Player1.x = 4;
                    Player1.y = 1;
                }

                if(EnteredEast == true)
                {
                    Room.CurrentRoom = Room.RoomDict[7];
                    CreateMap();
                    Player1.x = 1;
                    Player1.y = 3;
                }


            }

            if(Room.CurrentRoom == Room.RoomDict[7])
            {
                bool EnteredEast = Player1.x == PassagesInfo[1].x && Player1.y == PassagesInfo[1].y;
                bool EnteredWest = Player1.x == PassagesInfo[0].x && Player1.y == PassagesInfo[0].y;

                if(EnteredWest == true)
                {
                    Room.CurrentRoom = Room.RoomDict[6];
                    CreateMap();
                    Player1.x = 7;
                    Player1.y = 3;
                }

                if(EnteredEast == true)
                {
                    Room.CurrentRoom = Room.RoomDict[2];
                    CreateMap();
                    Player1.x = 1;
                    Player1.y = 3;
                }

            }




        }



    }
}