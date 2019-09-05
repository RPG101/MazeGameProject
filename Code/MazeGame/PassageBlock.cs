using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal class PassageBlock : BackgroundBlock
    {
        //constructor for passage block
        public PassageBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {

        }

        //sets the block to be solid or not.
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

        // checks this block is a passage to a different room.
        public override bool IsPassage
        {
            get
            {
                return true;
            }
        }

        // renders the passageblock as a teal rectangle
        public override void RenderBlock(Graphics lGraphics)
        {
            SolidBrush fill = new SolidBrush(Color.Teal);
            lGraphics.FillRectangle(fill, new Rectangle(base.BlockXvalue * Blockwidth, base.BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
