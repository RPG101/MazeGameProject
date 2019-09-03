using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    internal class CurrencyBlock : BackgroundBlock
    {
        public CurrencyBlock(int CurrentX, int CurrentY, int CurrentWidth, int CurrentHeight) : base(CurrentX, CurrentY, CurrentWidth, CurrentHeight)
        {

        }

        public static int CurrentX { get; set; }
        public static int CurrentY { get; set; }

        private static bool IsGoldCoin { get; set; }
        private static bool IsSilverCoin { get; set; }
        private static bool IsBronzeCoin { get; set; }

        public override bool SolidBlock
        {
            get
            {
                return false;
            }
        }

        public override void RenderBlock(Graphics lGraphics)
        {
            SolidBrush fill = null;
            if (IsGoldCoin == true)
            {
                fill = new SolidBrush(Color.Gold);
            } else if(IsSilverCoin == true)
            {
                fill = new SolidBrush(Color.Silver);
            } else if (IsBronzeCoin == true)
            {
                fill = new SolidBrush(Color.Brown);
            }
            
            lGraphics.FillRectangle(fill, new Rectangle(BlockXvalue * Blockwidth, BlockYvalue * Blockheight, Blockwidth, Blockheight));

        }

    }
}
