using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    class Rook : Piece
    {
        public override void ShowPossibleMove()
        {
            var isWhite = this.obj.name.Contains(Constant.White);
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int di = 0; di < dx.Length; di++)
            for (int i = 0; i < 8; i++)
            {
                var tmpx = this.x + (dx[di] * (i+1));
                var tmpy = this.y + (dy[di] * (i+1));

                if (tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                {
                    if (Util.GetPieceFromPieces(tmpx, tmpy) == null)
                        Chess_Board.GetInstance.AddPossibleMove(tmpx, tmpy);
                    else if (Util.GetPieceFromPieces(tmpx, tmpy) != null &&
                        (isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black) ||
                        !isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White)))
                    {
                        Chess_Board.GetInstance.AddPossibleMove(tmpx, tmpy);
                        break;
                    }
                    else
                        break;
                }
            }

            // TODO: castling
        }
    }
}
