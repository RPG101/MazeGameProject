using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    // wall block that inherits base class background block
    internal class WallBlock : BackgroundBlock
    {
        //wall block constuctor.
        public WallBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {

        }

        //sets wallblock to be a solid block
        public override bool SolidBlock
        {
            get
            {
                return true;
            }
        }

        //renders wallblock as a black square.
        public override void RenderBlock(Graphics lGraphics)
        {
            SolidBrush fill = new SolidBrush(Color.Black);
            lGraphics.FillRectangle(fill, new Rectangle(base.BlockXvalue * Blockwidth, base.BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
