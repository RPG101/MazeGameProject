using Microsoft.Build.BuildEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGame
{
    public partial class GameForm : Form
    {
        //creates a new game map object from the GenerateGame class.
        private GenerateGame GameMap;

        public GameForm()
        {
            //default windows forms initialization.
            InitializeComponent();
            // enable key preview and keydown for handle's in key presses.
            this.KeyPreview = true;
            this.KeyDown += Gameform_KeyDown;

        }

        //key press function, if a key is pressed update the current movement value.
        private void Gameform_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GameMap.PlayerAttack = GenerateGame.PlayerAction.NoAction;
                    GameMap.PlayerMovement = GenerateGame.Movement.UP;
                    break;
                case Keys.Down:
                    GameMap.PlayerAttack = GenerateGame.PlayerAction.NoAction;
                    GameMap.PlayerMovement = GenerateGame.Movement.DOWN;
                    break;
                case Keys.Left:
                    GameMap.PlayerAttack = GenerateGame.PlayerAction.NoAction;
                    GameMap.PlayerMovement = GenerateGame.Movement.LEFT;
                    break;
                case Keys.Right:
                    GameMap.PlayerAttack = GenerateGame.PlayerAction.NoAction;
                    GameMap.PlayerMovement = GenerateGame.Movement.RIGHT;
                    break;
                case Keys.Space:
                    GameMap.PlayerAttack = GenerateGame.PlayerAction.club;
                    GameMap.PlayerMovement = GenerateGame.Movement.None;
                    break;
                case Keys.C:
                    GameMap.PlayerAttack = GenerateGame.PlayerAction.collect;
                    GameMap.PlayerMovement = GenerateGame.Movement.None;
                    break;
                default:
                    break;
            }
            //call to update the game once movement has occured.
            updateGame();
        }

        // exits the application if the user hits the window X button.
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            switch (MessageBox.Show(this, "Thanks for Playing, Closing application!", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    Application.Exit();
                    break;
            }
        }

        //sets the game objects and map to load when the form is being loaded.
        private void GameForm_Load(object sender, EventArgs e)
        {
            GameMap = new GenerateGame(MazeDisplayBox);
            GameMap.CreateMap();
            updateGame();
        }

        //updates the game calling the update function to re-render the map and player upon movement or other conditions such as threat destroyed or coin collected.
        private void updateGame()
        {
            GameMap.UpdateGame();
            Healthbox.Text = GameMap.Player1.health.ToString();
            if (GameMap.GameWin == true)
            {
                MessageBox.Show("You found the exit! congratulations!" + "\n" + "You got a x / x Wealth" + "\n" + "Restarting Game!");
            }


        }

        private void button1_MouseClick(object sender, EventArgs e)
        {
            GameForm NewForm = new GameForm();
            NewForm.Show();
            this.Dispose(false);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
