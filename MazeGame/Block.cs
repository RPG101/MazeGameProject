using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal interface IBlock
    {
        int Blockwidth { get; }

        int Blockheight { get; }

        int BlockXvalue { get; }

        int BlockYvalue { get; }

        bool SolidBlock { get; }

        uint Pointvalue { get; }


        void RenderBlock(Graphics lGraphics);

    }
}
