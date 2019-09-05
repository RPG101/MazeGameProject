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

        public enum PlayerAction
        {
            NoAction,
            club,
            collect
        }

        // A struct to store the player object information such as x, y values and the current movement value
        public struct Player
        {
            public int x;
            public int y;
            public int health;
            public int Wealth;
            public PlayerAction PlayerAttack;
            public Movement PlayerDirection;
        }

        // A struct to store the Threats information, its x and y values and its current movement value
        public struct Threat
        {
            public int x;
            public int y;
            public bool IsDestroyed;
            public Movement ThreatDirection;
        }

        public struct Coin
        {
            public int x;
            public int y;
            public int Value;
            public SolidBrush CoinColor;
            public string CoinType;
            public bool BeenCollected;
        }

        public Dictionary<int, Room> RoomDict = new Dictionary<int, Room>();
        // data for each block default WxH
        private const int CurrentWidth = 30;
        private const int CurrentHeight = 30;
        // creates a block array to store each block for the picture box locaiton.
        private IBlock[,] GameRegions;
        //creates a picturebox to add graphics to the display, this will be updated into the main form.
        private PictureBox MazeDisplayBox;
        private readonly Room[] Rooms = new Room[8];
        private int CurrentRoom = 0;
        //player movement to set current direction based on key pressed.
        public Movement PlayerMovement { set { Player1.PlayerDirection = value; } }
        public PlayerAction PlayerAttack { set { Player1.PlayerAttack = value; } }
        // creates a player object to store current x and y values + movement.
        public Player Player1;
        public Threat Enemy;
        public Player UpdatedPlayerAction;
        public Coin Coins;
        // creates a new player object to store the updated x and y values when movement occurs. 
        public Player Updatedplayer;
        //boolean to set the game win condition.
        public bool GameWin { get; private set; } = false;

        public void LoadRooms()
        {
            int MapNumber = 0;
            string[] RoomPaths = Directory.GetFiles(@"..\..\..\Levels\");
            // iterate over each room string 
            foreach (string room in RoomPaths)
            {
                // checks if the file exists.
                if (File.Exists(room))
                {
                    // creates a string to store all text from file.
                    string lines = File.ReadAllText(room);

                    // creates storage for each x and y value
                    int x = 0, y = 0;
                    int[,] MapLoadArray = new int[9, 9];
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
                    Rooms[MapNumber] = new Room(MapLoadArray);
                    MapNumber++;
                }
            }
            int key = 0;
            foreach (Room Room in Rooms)
            {
                RoomDict.Add(key, Room);
                key++;
            }
        }

        public void CreateMap()
        {
            Random RandNumber = new Random();
            Coins.Value = RandNumber.Next(1, 100);
            UpdateCoinData();

            //loads a random room from the room dictonary
            int[,] LoadedMapArray = Rooms[CurrentRoom].Map;

            //loops over the x and y values loaded in the map
            for (int CurrentX = 0; CurrentX < LoadedMapArray.GetLength(0); CurrentX++)
            {
                for (int CurrentY = 0; CurrentY < LoadedMapArray.GetLength(1); CurrentY++)
                {
                    // if the file contains a 0 set this as a background block
                    if (LoadedMapArray[CurrentX, CurrentY] == 0)
                    {
                        GameRegions[CurrentX, CurrentY] = new BackgroundBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                        Random RandLocation = new Random();
                        Enemy.x = RandLocation.Next(1, 6);
                        Enemy.y = RandLocation.Next(1, 6);
                        Coins.x = RandLocation.Next(1, 6);
                        Coins.y = RandLocation.Next(1, 6);
                        if(Coins.x == Player1.x && Coins.y == Player1.y)
                        {
                            
                            Coins.x = RandLocation.Next(1, 6);
                            Coins.y = RandLocation.Next(1, 6);
                        }
                        if (Coins.x == Enemy.x && Coins.y == Enemy.y)
                        {
                            Coins.x = RandLocation.Next(1, 6);
                            Coins.y = RandLocation.Next(1, 6);
                        }
                    
                        if (Enemy.x == Player1.x && Enemy.y == Player1.y)
                        {
                            Enemy.x = RandLocation.Next(1, 6);
                            Enemy.y = RandLocation.Next(1, 6);
                        }
                    }
                    // if the file contains a 1 set this as a wall block.
                    if (LoadedMapArray[CurrentX, CurrentY] == 1)
                    {
                        GameRegions[CurrentX, CurrentY] = new WallBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                    }
                    //if the file contains a 2 set this as a passage block
                    if (LoadedMapArray[CurrentX, CurrentY] == 2 || LoadedMapArray[CurrentX, CurrentY] == 3 || LoadedMapArray[CurrentX, CurrentY] == 4 || LoadedMapArray[CurrentX, CurrentY] == 5)
                    {
                        GameRegions[CurrentX, CurrentY] = new PassageBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                    }
                    //if the file contains a 3 set this as an exit block.
                    if (LoadedMapArray[CurrentX, CurrentY] == 6)
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
            Random RandNumber = new Random();
            Player1.x = RandNumber.Next(1, 6);
            Player1.y = RandNumber.Next(1, 6);
            Player1.health = 100;
            //loads all room configuration files.
            LoadRooms();
            SetRoomLinks();
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

            if(Rooms[CurrentRoom].NoThreats == false)
            {
                SolidBrush EnemyColor = new SolidBrush(Color.Red);
                MapGraphic.FillRectangle(EnemyColor, new Rectangle(Enemy.x * CurrentWidth, Enemy.y * CurrentHeight, CurrentWidth, CurrentHeight));
            }

            if(Rooms[CurrentRoom].NoCoins == false)
            {

                MapGraphic.FillRectangle(Coins.CoinColor, new Rectangle(Coins.x * CurrentWidth, Coins.y * CurrentHeight, CurrentWidth, CurrentHeight));
            } 

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
                case Movement.None:
                    break;
                default:
                    break;
            }
            //provents movement through walls, this is done if the updated player location is a wall then just return the current player without changes
            if (GameRegions[Updatedplayer.x, Updatedplayer.y].SolidBlock == true) return lplayer;

            // if the location is not a wall from previous statment then the player location is updated correctly
            return Player1 = Updatedplayer;
        }

        public void CoinMessageAndRemove(int Coin, string CoinType)
        {
            
            MessageBox.Show("You found a " + CoinType + " Coin Worth " + Coin + " Points!", "closing", MessageBoxButtons.OK);
            Coins.BeenCollected = true;
            Rooms[CurrentRoom].NoCoins = true;
            Coins.x = 0;
            Coins.y = 0;
        }


        public Player UpdateAction(Player lplayer)
        {
            Random RandomNumber = new Random();
            
            UpdatedPlayerAction = lplayer;

            switch (UpdatedPlayerAction.PlayerAttack)
            {
                case PlayerAction.club:
                    if(Enemy.x == Player1.x && Enemy.y == Player1.y)
                    {
                        int DamageTaken = RandomNumber.Next(1, 5);
                        UpdatedPlayerAction.health -= DamageTaken;
                        MessageBox.Show("You Defeated the enemy with a mighty Club, You took "  + DamageTaken  + " points of damage from the fight!", "closing", MessageBoxButtons.OK);
                        Enemy.IsDestroyed = true;
                        Rooms[CurrentRoom].NoThreats = true;
                        Enemy.x = 0;
                        Enemy.y = 0;
                    }
                    break;
                case PlayerAction.collect:
                    if (Coins.x == Player1.x && Coins.y == Player1.y)
                    {
                        UpdatedPlayerAction.Wealth += Coins.Value;
                         CoinMessageAndRemove(Coins.Value, Coins.CoinType);
                        
                    }
                    break;
                case PlayerAction.NoAction:
                    break;
                default:
                    break;

            }
            return Player1 = UpdatedPlayerAction;

        }

        public void UpdateCoinData()
        {
            if (Coins.Value >= 1 && Coins.Value <= 49)
            {
                Coins.CoinType = "Copper";
                Coins.CoinColor = new SolidBrush(Color.Brown);
            }
            if (Coins.Value >= 50 && Coins.Value <= 75)
            {
                Coins.CoinType = "Silver";
                Coins.CoinColor = new SolidBrush(Color.Silver);
            }
            if (Coins.Value >= 76 && Coins.Value <= 100)
            {
                Coins.CoinType = "Gold";
                Coins.CoinColor = new SolidBrush(Color.Gold);
            }
        }

        public void UpdateGame()
        {
            //calls the code to update a player's move
            UpdateMove(Player1);
            UpdateAction(Player1);
            //renders the map if changes have occured e.g. threat defeated, coin obtained or player moved. all need to be re-displayed on the screen
            PassageLocate();
            Rendermap();
            //sets the win condition for the exit block.

            GameWin = Player1.x == ExitBlock.CurrentX && Player1.y == ExitBlock.CurrentY;

        }

        public Room LinkRooms(int X, int Y)
        {
            Room UpdatedRoom = Rooms[CurrentRoom];

            if (UpdatedRoom.Map[X,Y] == 2 && UpdatedRoom.NoThreats == true)
            {
                GameRegions[X, Y].SolidBlock = false;
                UpdatedRoom = RoomDict[UpdatedRoom.North];
                Player1.x = 4;
                Player1.y = 6;
            }
            if (Rooms[CurrentRoom].Map[X, Y] == 2 && Rooms[CurrentRoom].NoThreats == false)
            {
                GameRegions[X, Y].SolidBlock = true;
                MessageBox.Show("You Cannot go that way, All Enemy's must be defeated", "closing", MessageBoxButtons.OK);
                Player1.y += 1;

            }

            if(UpdatedRoom.Map[X, Y] == 3 && UpdatedRoom.NoThreats == true)
            {
                GameRegions[X, Y].SolidBlock = false;
                UpdatedRoom = RoomDict[UpdatedRoom.West];
                Player1.x = 7;
                Player1.y = 3;
            }
            if (Rooms[CurrentRoom].Map[X, Y] == 3 && Rooms[CurrentRoom].NoThreats == false)
            {
                GameRegions[X, Y].SolidBlock = true;
                MessageBox.Show("You Cannot go that way, All Enemy's must be defeated", "closing", MessageBoxButtons.OK);
                Player1.x += 1;
            }

            if (UpdatedRoom.Map[X, Y] == 4 && UpdatedRoom.NoThreats == true)
            {
                GameRegions[X, Y].SolidBlock = false;
                UpdatedRoom = RoomDict[UpdatedRoom.south];
                Player1.x = 4;
                Player1.y = 1;
            }
            if (Rooms[CurrentRoom].Map[X, Y] == 4 && Rooms[CurrentRoom].NoThreats == false)
            {
                GameRegions[X, Y].SolidBlock = true;
                MessageBox.Show("You Cannot go that way, All Enemy's must be defeated", "closing", MessageBoxButtons.OK);
                Player1.y -= 1;
            }

            if (UpdatedRoom.Map[X, Y] == 5 && UpdatedRoom.NoThreats == true)
            {
                GameRegions[X, Y].SolidBlock = false;
                UpdatedRoom = RoomDict[UpdatedRoom.East];
                Player1.x = 1;
                Player1.y = 3;
            }
            if (Rooms[CurrentRoom].Map[X, Y] == 5 && Rooms[CurrentRoom].NoThreats == false)
            {
                GameRegions[X, Y].SolidBlock = true;
                MessageBox.Show("You Cannot go that way, All Enemy's must be defeated", "closing", MessageBoxButtons.OK);
                Player1.x -= 1;
            }

            return UpdatedRoom;
        }

       


        public void SetRoomLinks()
        {
            Rooms[0].North = 1;
            Rooms[0].West = 5;
            Rooms[1].North = 2;
            Rooms[1].south = 0;
            Rooms[1].West = 3;
            Rooms[2].south = 1;
            Rooms[2].West = 7;
            Rooms[3].East = 1;
            Rooms[3].south = 5;
            Rooms[3].West = 4;
            Rooms[4].East = 3;
            Rooms[4].North = 6;
            Rooms[5].North = 3;
            Rooms[5].East = 0;
            Rooms[6].south = 4;
            Rooms[6].East = 7;
            Rooms[7].West = 6;
            Rooms[7].East = 2;
        }

        public void PassageLocate()
        {
            if (GameRegions[Player1.x, Player1.y].IsPassage == true)
            {
                Room UpdateCurrent = Rooms[CurrentRoom];
                UpdateCurrent= LinkRooms(Player1.x, Player1.y);

                if(Rooms[CurrentRoom] == UpdateCurrent)
                {
                    return;
                } else
                {
                    Rooms[CurrentRoom] = UpdateCurrent;
                    CreateMap();
                }
               
            }

        }



    }
}