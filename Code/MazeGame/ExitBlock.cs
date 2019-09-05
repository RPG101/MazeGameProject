using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal class ExitBlock : BackgroundBlock
    {
        //constructor for exit block
        public ExitBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {
            X = CurrentX;
            Y = CurrentY;
        }
        //return the x and y values of the exit block
        public static int X { get; set; }
        public static int Y { get; set; }

        //set the exit block to not be solid
        public override bool SolidBlock
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        //renders block to screen, as a green rectangle
        public override void RenderBlock(Graphics lGraphics)
        {
            SolidBrush fill = new SolidBrush(Color.Green);
            lGraphics.FillRectangle(fill, new Rectangle(base.BlockXvalue * Blockwidth, base.BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
