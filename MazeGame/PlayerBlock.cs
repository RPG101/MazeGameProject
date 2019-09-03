using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal class PlayerBlock : BackgroundBlock
    {
        public PlayerBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {

        }

        public static int CurrentX { get; set; }
        public static int CurrentY { get; set; }

        public enum Movement
        {
            None,
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public override bool SolidBlock
        {
            get
            {
                return true;
            }
        }

        public override void RenderBlock(Graphics lGraphics)
        {
            SolidBrush fill = new SolidBrush(Color.Blue);
            lGraphics.FillRectangle(fill, new Rectangle(BlockXvalue * Blockwidth, BlockYvalue * Blockheight, Blockwidth, Blockheight));

        }

    }
}
