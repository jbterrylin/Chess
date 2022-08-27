using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Pieces
{
    class Knight : Piece
    {
        public override List<int[]> GetPossibleMove()
        {
            List<int[]> possibleMoves = new();
            var isWhite = this.obj.name.Contains(Constant.White);
            int[] dx = { -2, -1, 1, 2, -2, -1, 1, 2 };
            int[] dy = { -1, -2, -2, -1, 1, 2, 2, 1 };

            for(int i=0; i< dx.Length; i++)
            {
                var tmpx = this.x + dx[i];
                var tmpy = this.y + dy[i];

                if(tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                {
                    if (Util.GetPieceFromPieces(tmpx, tmpy) != null &&
                    (isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black) ||
                    !isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White)))
                        possibleMoves.Add(new int[2] { tmpx, tmpy });
                    else if (Util.GetPieceFromPieces(tmpx, tmpy) == null)
                        possibleMoves.Add(new int[2] { tmpx, tmpy });
                }
            }

            return possibleMoves;
        }

        public override List<int[]> GetKillPos()
        {
            List<int[]> killPos = new();
            int[] dx = { -2, -1, 1, 2, -2, -1, 1, 2 };
            int[] dy = { -1, -2, -2, -1, 1, 2, 2, 1 };

            for (int i = 0; i < dx.Length; i++)
            {
                var tmpx = this.x + dx[i];
                var tmpy = this.y + dy[i];

                if (tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                    killPos.Add(new int[2] { tmpx, tmpy });
            }

            return killPos;
        }

        public override bool CheckCheck(int x, int y)
        {
            return false;
        }
    }
}
