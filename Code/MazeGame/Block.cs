using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    /* interface to define and initialize the base class of Backgroundblock for inheritance. This will store information on
     the block height, width, x and y value and also if the block is a solid block (player Cannot move on a solid block)
     and the value for wealth. Also contains the Render block function. */

    internal interface IBlock
    {
        int Blockwidth { get; }

        int Blockheight { get; }

        int BlockXvalue { get; }

        int BlockYvalue { get; }

        bool SolidBlock { get; set; }

        bool IsPassage { get; }

        uint Pointvalue { get; }


        void RenderBlock(Graphics lGraphics);

    }
}
