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

        public ExitBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {

        }

        public static int CurrentX { get; set; }
        public static int CurrentY { get; set; }

        public override bool SolidBlock
        {
            get
            {
                return false;
            }
        }

        public override void RenderBlock(Graphics lGraphics)
        {
            SolidBrush fill = new SolidBrush(Color.Green);
            lGraphics.FillRectangle(fill, new Rectangle(base.BlockXvalue * Blockwidth, base.BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
