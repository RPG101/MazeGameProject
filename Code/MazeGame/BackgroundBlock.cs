using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal class BackgroundBlock : IBlock
    {
        public int BlockXvalue { get; protected set; }
        public int BlockYvalue { get; protected set; }
        public int Blockwidth { get; protected set; }
        public int Blockheight { get; protected set; }


        public BackgroundBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight)
        {
            this.BlockXvalue = CurrentX;
            this.BlockYvalue = CurrentY;
            this.Blockwidth = CurrentWidth;
            this.Blockheight = CurrentHeight;
        }

        public virtual bool SolidBlock
        {
            get
            {
                return false;
            }
        }

        public virtual uint Pointvalue
        {
            get
            {
                return 0;
            }
        }


        public virtual void RenderBlock(Graphics lGraphics)
        {
            lGraphics.DrawRectangle(Pens.DimGray, new Rectangle(BlockXvalue * Blockwidth, BlockYvalue * Blockheight, Blockwidth, Blockheight));
        }

    }
}
