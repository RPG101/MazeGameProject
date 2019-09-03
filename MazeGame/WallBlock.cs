using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal class WallBlock : BackgroundBlock
    {

        public WallBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {

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
            SolidBrush fill = new SolidBrush(Color.Black);
            lGraphics.FillRectangle(fill, new Rectangle(base.BlockXvalue * Blockwidth, base.BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
