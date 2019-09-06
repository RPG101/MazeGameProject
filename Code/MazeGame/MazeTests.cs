using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;


namespace MazeGame
{
    [SetUpFixture]
    public class Setup
    {
        // setup to set the needed classes for testing 
        public GameForm GameForms = new GameForm();
        
        [OneTimeSetUp]
        public void Before()
        {
           //sets the current directory to the current bin folder in use. (provents pathing issues from test log of Nunit being in Visualstudio temp folder);
          var location = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
          Directory.SetCurrentDirectory(location);
          //Initialises the game map object.
          GameForms.GameMap = new GenerateGame(GameForms.MazeDisplayBox);

        }
    }
    

    [TestFixture] public class MazeTests : Setup
    { 
        [Test]
        public virtual void TestRoomsTotal()
        {
            //Integer to compair the game rooms lenght to.
            int RoomTotal = 8;
            Assert.AreEqual(RoomTotal, GameForms.GameMap.Rooms.Length);
        }

        [Test]
        public virtual void TestRoomMapDimentions()
        {
            // values of what the rooms map lenght should be and then iterate over each room for its map x/y axis to get the total for comparison.
            int x = 9;
            int y = 9;

            for (int i = 0; i < GameForms.GameMap.Rooms.Length; i++)
            {
                Assert.AreEqual(x, GameForms.GameMap.Rooms[i].Map.GetLength(0));
                Assert.AreEqual(y, GameForms.GameMap.Rooms[i].Map.GetLength(1));
            }
        }

        [Test]
        public virtual void TestGameRegions()
        {
            // checks the games regions are correct to the same size as what the room map should be.
            int x = 9;
            int y = 9;

            Assert.AreEqual(x, GameForms.GameMap.GameRegions.GetLength(0));
            Assert.AreEqual(y, GameForms.GameMap.GameRegions.GetLength(1));

        }

        [Test]
        public virtual void TestPlayerMove()
        {
            //initialise a map.
            GameForms.GameMap.CreateMap();

            //set player location
            GameForms.GameMap.Player1.x = 1;
            GameForms.GameMap.Player1.y = 1;

            //set player movement
            GameForms.GameMap.Player1.PlayerDirection = GenerateGame.Movement.DOWN;

            //call update movement function with the previously set values.
            GameForms.GameMap.UpdateMove(GameForms.GameMap.Player1);

            //checks the values are what is exspected.
            Assert.AreEqual(2, GameForms.GameMap.Player1.y);

            // set player movment
            GameForms.GameMap.Player1.PlayerDirection = GenerateGame.Movement.UP;
            //set player location
            GameForms.GameMap.Player1.x = 1;
            GameForms.GameMap.Player1.y = 1;
            //call update movement function with the previouslly set values
            GameForms.GameMap.UpdateMove(GameForms.GameMap.Player1);
            //check the values are what is exspected.
            Assert.AreEqual(1, GameForms.GameMap.Player1.y);

        }

        [Test]
        public virtual void TestPlayerAction()
        {
            //initialise a new map
            GameForms.GameMap.CreateMap();
            //set player action
            GameForms.GameMap.Player1.playerAction = GenerateGame.PlayerAction.club;
            //int for comparison with health value of player
            int maxhealth = 100;

            //set the player location and enemy location to the same (this allows if statments to continue to check if value of health has changed)
            GameForms.GameMap.Player1.x = 1;
            GameForms.GameMap.Player1.y = 1;
            GameForms.GameMap.Enemy.x = 1;
            GameForms.GameMap.Enemy.y = 1;

            //Call the update action function with previously set vairables.
            GameForms.GameMap.UpdateAction(GameForms.GameMap.Player1);
            //assert the health value has been lowered.
            Assert.Less(GameForms.GameMap.Player1.health, maxhealth);
        }

        [Test]
        public virtual void TestCoinValue()
        {
            /*With this test, the first random generation for the coin value will always be the same due to the seed being set to a fixed seed for testing purposes */

            //intialise current map
            GameForms.GameMap.CreateMap();
            //set known value of seed
            int TestValue = 59;
            //check value is correct
            Assert.AreEqual(TestValue, GameForms.GameMap.Coins[GameForms.GameMap.CurrentRoom]);

        }
        [Test]
        public virtual void TestLoad()
        {
            //clear any currently loaded rooms when initialising the gamemap;
            GameForms.GameMap.RoomDict.Clear();

            //call the load rooms function;
            GameForms.GameMap.LoadRooms();

            //comparison value.
            int AmountOfRooms = 8;

            //check current paths loaded for room files are correct size.
            Assert.AreEqual(AmountOfRooms, GameForms.GameMap.RoomPaths.Length);

        }
    }
}
