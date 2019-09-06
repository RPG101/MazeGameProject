using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace MazeGame
{
    public class GenerateGame
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
        //enum for a players actions.
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
            public PlayerAction playerAction;
            public Movement PlayerDirection;
        }

        // A struct to store the Threats information, its x and y values and its current movement value
        public struct Threat
        {
            public int x;
            public int y;
            public bool IsDestroyed;
        }

        //struct to store the coin data
        public struct Coin
        {
            public int x;
            public int y;
            public int Value;
            public SolidBrush CoinColor;
            public string CoinType;
            public bool BeenCollected;
        }

        /* initializes the objects and vairables needed in other functions */
        public string[] RoomPaths = Directory.GetFiles(@"..\..\..\Levels\");
        public Dictionary<int, Room> RoomDict = new Dictionary<int, Room>();
        private const int CurrentWidth = 30;
        private const int CurrentHeight = 30; 
        public IBlock[,] GameRegions;
        private PictureBox MazeDisplayBox;
        bool Firstload;
        public int[,] LoadedMapArray;
        public readonly Room[] Rooms = new Room[8];
        public static Random RandomSpawn = new Random(6);
        private int CurrentRoom = RandomSpawn.Next(0, 6);
        public Movement PlayerMovement { set { Player1.PlayerDirection = value; } }
        public PlayerAction PlayerAttack { set { Player1.playerAction = value; } }
        public Player Player1;
        public Threat Enemy;
        public Player UpdatedPlayerAction;
        public Coin Coins;
        public Player Updatedplayer;
        public bool GameWin { get; set; } = false;


        //loads all rooms at once and stores room map data into room object then stores all rooms into a dictionary.
        public void LoadRooms()
        {
            int MapNumber = 0;
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

        /*this function sets all the blocks in the map from the 
        values in the text file then calls the function to render each block, 
        this also will randomly set the coin and threat location.
        This also does some validation on the map file for each value in the map file.
        if the map files are incorrect the application will break*/
        public void CreateMap()
        {
            UpdateLocationsinMap();
            UpdateCoinData();

            LoadedMapArray = Rooms[CurrentRoom].Map;

            //loops over the x and y values loaded in the map
            for (int CurrentX = 0; CurrentX < LoadedMapArray.GetLength(0); CurrentX++)
            {
                for (int CurrentY = 0; CurrentY < LoadedMapArray.GetLength(1); CurrentY++)
                {
                    // if the file contains a 0 set this as a background block
                    if (LoadedMapArray[CurrentX, CurrentY] == 0)
                    {
                        // random locations for each coin and threat if they do not conflict with each other.
                        GameRegions[CurrentX, CurrentY] = new BackgroundBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
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
                    }
                }
            }
            // renders the map once all data has been loaded and set to a specific block
            Rendermap();
        }

        public GenerateGame(PictureBox GridGen)
        {
            //start up, to create the maze and its grid size for a room.
            this.MazeDisplayBox = GridGen;

            int GridpointX = 9;
            int GridpointY = 9;

            //Creates all grid points as a block
            GameRegions = new IBlock[GridpointX, GridpointY];

            //sets a random location for the player
            Player1.x = RandomSpawn.Next(1, 6);
            Player1.y = RandomSpawn.Next(1, 6);
            Enemy.x = RandomSpawn.Next(1, 6);
            Enemy.y = RandomSpawn.Next(1, 6);
            Coins.x = 6;
            Coins.y = 5;
            Player1.health = 100;
            //loads all room configuration files.
            LoadRooms();
            //sets how the rooms link together
            SetRoomLinks();
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

            //if no threats do not render a threat.
            if(Rooms[CurrentRoom].NoThreats == false)
            {
                SolidBrush EnemyColor = new SolidBrush(Color.Red);
                MapGraphic.FillRectangle(EnemyColor, new Rectangle(Enemy.x * CurrentWidth, Enemy.y * CurrentHeight, CurrentWidth, CurrentHeight));
            }

            //if no coins do not render a coin.
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
         // takes the coin value and type and displayes a message box when the user collects the coin.   
            MessageBox.Show("You found a " + CoinType + " Coin Worth " + Coin + " Points!", "closing", MessageBoxButtons.OK);
            Coins.BeenCollected = true;
            Rooms[CurrentRoom].NoCoins = true;
            Coins.x = 0;
            Coins.y = 0;
        }

        public void UpdateLocationsinMap()
        {
            Enemy.x = RandomSpawn.Next(1, 6);
            Enemy.y = RandomSpawn.Next(1, 6);
            Coins.x = 6;
            Coins.y = 5;

            if (Rooms[CurrentRoom].NoThreats == false && Rooms[CurrentRoom].NoCoins == false)
            {
                Coins.Value = RandomSpawn.Next(1, 100);
            }

            if (Enemy.x == Coins.x && Enemy.y == Coins.y)
            {
                Enemy.x = RandomSpawn.Next(1, 6);
                Enemy.y = RandomSpawn.Next(1, 6);
            }
            if(Enemy.x == Player1.x && Enemy.y == Player1.y)
            {
                Enemy.x = RandomSpawn.Next(1, 6);
                Enemy.y = RandomSpawn.Next(1, 6);
            }


        }

        public Player UpdateAction(Player lplayer)
        {
            Random RandomNumber = new Random();
            
            UpdatedPlayerAction = lplayer;

            switch (UpdatedPlayerAction.playerAction)
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
            //sets the values for coin type and color based on the value of the coin.
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
            UpdateExit();
            //renders the map if changes have occured e.g. threat defeated, coin obtained or player moved. all need to be re-displayed on the screen
            PassageLocate();
            Rendermap();
            //sets the win condition for the exit block.
            
            

        }

        public void UpdateExit()
        {
            // updates the exit room to not allow a player to enter exit if threats exist in the room.
            if (Rooms[CurrentRoom].Map == Rooms[7].Map && Rooms[7].NoThreats == true)
            {
                GameWin = Player1.x == ExitBlock.X && Player1.y == ExitBlock.Y;
            }
            if (Rooms[CurrentRoom].Map == Rooms[7].Map && Rooms[7].NoThreats == false && Player1.x == ExitBlock.X && Player1.y == ExitBlock.Y)
            {
                GameRegions[ExitBlock.X, ExitBlock.Y].SolidBlock = true;
                MessageBox.Show("You Cannot go that way, All Enemy's must be defeated", "closing", MessageBoxButtons.OK);
                Player1.y += 1;
            }
        }

        public Room LinkRooms(int X, int Y)
        {
            // links all rooms together and does not let the player exit a room if a threat exists.
            Room UpdatedRoom = Rooms[CurrentRoom];

            if (UpdatedRoom.Map[X,Y] == 2 && UpdatedRoom.NoThreats == true)
            {
                GameRegions[X, Y].SolidBlock = false;
                RoomDict[CurrentRoom].HasEntered = true;
                UpdatedRoom = RoomDict[UpdatedRoom.North];
                RoomDict[UpdatedRoom.North].HasEntered = true;
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
                RoomDict[CurrentRoom].HasEntered = true;
                UpdatedRoom = RoomDict[UpdatedRoom.West];
                RoomDict[UpdatedRoom.West].HasEntered = true;
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
                RoomDict[CurrentRoom].HasEntered = true;
                UpdatedRoom = RoomDict[UpdatedRoom.south];
                RoomDict[UpdatedRoom.south].HasEntered = true;
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
                RoomDict[CurrentRoom].HasEntered = true;
                UpdatedRoom = RoomDict[UpdatedRoom.East];
                RoomDict[UpdatedRoom.East].HasEntered = true;
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
            //sets the links for each room
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

        /* checks the players location in accordance to the grid to see if a player is attempting to enter a passage
           if the player room location is correct the room will be updated.*/
        public void PassageLocate()
        {
            if (GameRegions[Player1.x, Player1.y].IsPassage == true)
            {
                Room UpdateCurrent = Rooms[CurrentRoom];
                UpdateCurrent = LinkRooms(Player1.x, Player1.y);

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