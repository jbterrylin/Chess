using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts
{
    public class History
    {
        public int move;
        public string movePiece;
        public int oriX;
        public int oriY;
        public int newX;
        public int newY;
        public string killPiece;
        public string status;

        public History(string movePiece, int oriX, int oriY, int newX, int newY, string killPiece)
        {
            this.movePiece = movePiece;
            this.oriX = oriX;
            this.oriY = oriY;
            this.newX = newX;
            this.newY = newY;
            this.killPiece = killPiece;
        }

        public override string ToString()
        {
            return "Move " + move + ":\n" + movePiece + "-(" + Util.IntToAlpha(oriX + 1) + (oriY + 1) + ">" + Util.IntToAlpha(newX + 1) + (newY+1) + ")" + (killPiece != "" ? ("/" + killPiece) : "");
        }
    }
}
