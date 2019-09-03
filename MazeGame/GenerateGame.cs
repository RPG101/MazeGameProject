using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MazeGame
{
    internal class GenerateGame
    {
        public enum Movement
        {
            None,
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public struct Player
        {
            public int x;
            public int y;
            public Movement PlayerDirection;
        }

        public struct Threat
        {
            public int x;
            public int y;
            public Movement ThreatDirection;
        }

        private const int CurrentWidth = 30;
        private const int CurrentHeight = 30;
        private IBlock[,] GameRegions;
        private PictureBox MazeDisplayBox;
        public Movement PlayerMovement { set { Player1.PlayerDirection = value; } }
        static Random RandomNumber = new Random();
        public int Room = RandomNumber.Next(0, 11);
        public List<int> passagePoint = new List<int>();
        public PlayerBlock Player01;
        public Player Player1;
        public Player Updatedplayer;

        public bool GameWin { get; private set; } = false;
        public bool PassageEntered { get; private set; } = false;

        public int[,] LoadMap(string lInput)
        {
            int[,] MapLoadArray = new int[17, 14];

            string textFile = lInput;
            if (File.Exists(textFile))
            {
                string lines = File.ReadAllText(textFile);

                int i = 0, j = 0;

                foreach (var row in lines.Split('\n'))
                {
                    j = 0;
                    foreach (var col in row.Trim().Split(' '))
                    {
                        MapLoadArray[i, j] = int.Parse(col.Trim());
                        j++;
                    }
                    i++;
                }
            }
            GameRegions = new IBlock[MapLoadArray.GetLength(0), MapLoadArray.GetLength(1)];
            return MapLoadArray;
        }

        public void CreateMap()
        {
            int[,] LoadedMapArray = LoadMap(RoomSelect(Room));

            for (int CurrentX = 0; CurrentX < LoadedMapArray.GetLength(0); CurrentX++)
            {
                for (int CurrentY = 0; CurrentY < LoadedMapArray.GetLength(1); CurrentY++)
                {
                    if (LoadedMapArray[CurrentX, CurrentY] == 0)
                    {
                        GameRegions[CurrentX, CurrentY] = new BackgroundBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                    }

                    if (LoadedMapArray[CurrentX, CurrentY] == 1)
                    {
                        GameRegions[CurrentX, CurrentY] = new WallBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                    }

                    if (LoadedMapArray[CurrentX, CurrentY] == 2)
                    {
                        GameRegions[CurrentX, CurrentY] = new PassageBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                        foreach (IBlock p in GameRegions)
                        {
                            if(p == null)
                            {
                                break;
                            } else
                            if (p.isPassage == true && p != null)
                            {
                                PassageBlock.Dimentions = new[]
                                {
                                     new PassageBlock.Point { X = p.BlockXvalue, Y = p.BlockYvalue }
                                };
                               
                            }
                        }
                        
                    }
                    if (LoadedMapArray[CurrentX, CurrentY] == 3)
                    {
                        GameRegions[CurrentX, CurrentY] = new ExitBlock(CurrentX, CurrentY, CurrentWidth, CurrentHeight);
                        ExitBlock.CurrentX = CurrentX;
                        ExitBlock.CurrentY = CurrentY;
                    }
                }
            }
            Rendermap();
        }

        public GenerateGame(PictureBox GridGen)
        {
            this.MazeDisplayBox = GridGen;

            int GridpointX = 17;
            int GridpointY = 14;

            GameRegions = new IBlock[GridpointX, GridpointY];

            int RandomLocation;

            RandomLocation = RandomNumber.Next(GameRegions.GetLength(0));

            Player1.x = RandomLocation;
            Player1.y = RandomLocation;
            PlayerBlock.CurrentX = Player1.x;
            PlayerBlock.CurrentY = Player1.y;
            
        }

        public void Rendermap()
        {
            Bitmap MapImage = new Bitmap(MazeDisplayBox.Width, MazeDisplayBox.Height);

            Graphics MapGraphic = Graphics.FromImage(MapImage);

            MapGraphic.InterpolationMode = InterpolationMode.Low;
            MapGraphic.CompositingQuality = CompositingQuality.HighSpeed;

            foreach (IBlock Map in GameRegions)
            {
                 Map.RenderBlock(MapGraphic);
            }
            MazeDisplayBox.Image = MapImage;
            MazeDisplayBox.Refresh();
        }

        public void RenderPlayer(PlayerBlock lCurrentPlayer)
        {


            Graphics PlayerGraphic = MazeDisplayBox.CreateGraphics();
            PlayerGraphic.InterpolationMode = InterpolationMode.Low;
            PlayerGraphic.CompositingMode = CompositingMode.SourceOver;
            PlayerGraphic.CompositingQuality = CompositingQuality.HighSpeed;
            SolidBrush fill = new SolidBrush(Color.Blue);
            PlayerGraphic.FillRectangle(fill, new Rectangle(PlayerBlock.CurrentX * CurrentWidth, PlayerBlock.CurrentY * CurrentHeight, CurrentWidth, CurrentHeight));
        }

        public string RoomSelect(int lRoom)
        {


            switch (lRoom)
            {
                case 0:
                    return @"..\..\..\Levels\Level1.txt";
                case 1:
                    return @"..\..\..\Levels\Level1-Room1.txt";
                case 2:
                    return @"..\..\..\Levels\Level1-Room2.txt";
                case 3:
                    return @"..\..\..\Levels\Level1-Room3.txt";
                case 4:
                    return @"..\..\..\Levels\Level1-Room4.txt";
                case 5:
                    return @"..\..\..\Levels\Level1-Room5.txt";
                case 6:
                    return @"..\..\..\Levels\Level1-Room6.txt";
                case 7:
                    return @"..\..\..\Levels\Level1-Room7.txt";
                case 8:
                    return @"..\..\..\Levels\Level1-Room8.txt";
                case 9:
                    return @"..\..\..\Levels\Level1-Room9.txt";
                case 10:
                    return @"..\..\..\Levels\Level1-Room10.txt";
                case 11:
                    return @"..\..\..\Levels\Level1-Room11.txt";
                default:
                    return @"..\..\..\Levels\Level1-Complete.txt";
            }
        }

        public Player UpdateMove(Player lplayer)
        {
            Updatedplayer = lplayer;

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
            if (GameRegions[Updatedplayer.x, Updatedplayer.y].SolidBlock == true) return lplayer;

            PlayerBlock.CurrentX = Updatedplayer.x;
            PlayerBlock.CurrentY = Updatedplayer.y;
            return Player1 = Updatedplayer;
        }

        public void UpdateGame()
        {
            Rendermap();
            UpdateMove(Player1);
            RenderPlayer(Player01);
            for(int i =0; i < PassageBlock.Dimentions.Length; i++)
            {
                    PassageEntered = PlayerBlock.CurrentX == PassageBlock.Dimentions[i].X && PlayerBlock.CurrentY == PassageBlock.Dimentions[i].Y;
            }
            GameWin = PlayerBlock.CurrentX == ExitBlock.CurrentX && PlayerBlock.CurrentY == ExitBlock.CurrentY;
        }
    }
}