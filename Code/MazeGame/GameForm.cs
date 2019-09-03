using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGame
{
    public partial class GameForm : Form
    {
        private GenerateGame GameMap;

        public GameForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Gameform_KeyDown;
        }

        private void Gameform_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    GameMap.PlayerMovement = GenerateGame.Movement.UP;
                    break;
                case Keys.Down:
                    GameMap.PlayerMovement = GenerateGame.Movement.DOWN;
                    break;
                case Keys.Left:
                    GameMap.PlayerMovement = GenerateGame.Movement.LEFT;
                    break;
                case Keys.Right:
                    GameMap.PlayerMovement = GenerateGame.Movement.RIGHT;
                    break;
                default:
                    break;
            }
            updateGame();
        }

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
                    break;
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            GameMap = new GenerateGame(MazeDisplayBox);
            GameMap.CreateMap();
            updateGame();
        }

        private void updateGame()
        {
            GameMap.UpdateGame();
            if (GameMap.GameWin == true)
            {
                MessageBox.Show("You found the exit! congratulations!" + "\n" + "You got a x / x Wealth" + "\n" + "Restarting Game!");
                

            }
            if (GameMap.PassageEntered == true)
            {
                GameMap.CreateMap();
                GameMap.UpdateGame();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
