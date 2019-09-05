using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    //interits block class to alter with polymorphism.
    internal class BackgroundBlock : IBlock
    {
        public int BlockXvalue { get; protected set; }
        public int BlockYvalue { get; protected set; }
        public int Blockwidth { get; protected set; }
        public int Blockheight { get; protected set; }

        //constructor to set the backgroundblock values.
        public BackgroundBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight)
        {
            this.BlockXvalue = CurrentX;
            this.BlockYvalue = CurrentY;
            this.Blockwidth = CurrentWidth;
            this.Blockheight = CurrentHeight;
        }

        //bool to say if the block is solid (player cannot pass a solid block)
        public virtual bool SolidBlock
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        //check if this is a passage to a different room
        public virtual bool IsPassage
        {
            get
            {
                return false;
            }
        }

        // renders this block as a grid.
        public virtual void RenderBlock(Graphics lGraphics)
        {
            lGraphics.DrawRectangle(Pens.DimGray, new Rectangle(BlockXvalue * Blockwidth, BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
