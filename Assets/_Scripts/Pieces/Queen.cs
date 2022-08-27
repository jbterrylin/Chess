using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Pieces
{
    class Queen : Piece
    {
        public override List<int[]> GetPossibleMove()
        {
            List<int[]> possibleMoves = new();
            var isWhite = this.obj.name.Contains(Constant.White);
            int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };

            for (int di = 0; di < dx.Length; di++)
            for (int i = 0; i < 8; i++)
            {
                var tmpx = this.x + (dx[di] * (i + 1));
                var tmpy = this.y + (dy[di] * (i + 1));

                if (tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                {
                    if (Util.GetPieceFromPieces(tmpx, tmpy) == null)
                            possibleMoves.Add(new int[2] { tmpx, tmpy });
                    else if (isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black) ||
                        !isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White))
                    {
                        possibleMoves.Add(new int[2] { tmpx, tmpy });
                        break;
                    }
                    else
                        break;
                }
            }

            return possibleMoves;
        }

        public override List<int[]> GetKillPos()
        {
            List<int[]> killPos = new();
            var isWhite = this.obj.name.Contains(Constant.White);
            int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };

            for (int di = 0; di < dx.Length; di++)
                for (int i = 0; i < 8; i++)
                {
                    var tmpx = this.x + (dx[di] * (i + 1));
                    var tmpy = this.y + (dy[di] * (i + 1));

                    // else if checking is for prevent king move to same piece kill position (lazer beam)
                    if (tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                    {
                        if (Util.GetPieceFromPieces(tmpx, tmpy) == null)
                            killPos.Add(new int[2] { tmpx, tmpy });
                        else if (isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black + "_" + Constant.King) ||
                            !isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White + "_" + Constant.King))
                            killPos.Add(new int[2] { tmpx, tmpy });
                        else if (isWhite && !Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black) ||
                            !isWhite && !Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White))
                        {
                            killPos.Add(new int[2] { tmpx, tmpy });
                            break;
                        }
                    }
                }

            return killPos;
        }

        public override bool CheckCheck(int x, int y)
        {
            return false;
        }
    }
}
