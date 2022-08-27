using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Pieces
{
    class Pawn : Piece
    {
        public override List<int[]> GetPossibleMove()
        {
            List<int[]> possibleMoves= new();
            int direction = 0;
            if (this.obj.name.Contains(Constant.White))
                direction++;
            else
                direction--;

            // normal move
            if (Util.GetPieceFromPieces(this.x, this.y + direction) == null)
            {
                possibleMoves.Add(new int[2] { this.x, this.y + direction });

                // charge
                if (
                    ((this.obj.name.Contains(Constant.White) && this.y == 1) ||
                    (this.obj.name.Contains(Constant.Black) && this.y == 6)) &&
                    Util.GetPieceFromPieces(this.x, this.y + direction + direction) == null
                )
                {
                    possibleMoves.Add(new int[2] { this.x, this.y + direction + direction });
                }
            }

            // take piece
            var isWhite = this.obj.name.Contains(Constant.White);
            if (Util.GetPieceFromPieces(this.x + 1, this.y + direction) != null)
            {
                if (isWhite && Util.GetPieceFromPieces(this.x + 1, this.y + direction).obj.name.Contains(Constant.Black) ||
                    !isWhite && Util.GetPieceFromPieces(this.x + 1, this.y + direction).obj.name.Contains(Constant.White))
                {
                    possibleMoves.Add(new int[2] { this.x + 1, this.y + direction });
                }
            }
            if (Util.GetPieceFromPieces(this.x - 1, this.y + direction) != null)
            {
                if (isWhite && Util.GetPieceFromPieces(this.x - 1, this.y + direction).obj.name.Contains(Constant.Black) ||
                    !isWhite && Util.GetPieceFromPieces(this.x - 1, this.y + direction).obj.name.Contains(Constant.White))
                {
                    possibleMoves.Add(new int[2] { this.x - 1, this.y + direction });
                }
            }

            // TODO: En passant
            return possibleMoves;
        }

        public override List<int[]> GetKillPos()
        {
            List<int[]> possibleMoves = new();
            int direction = 0;
            if (this.obj.name.Contains(Constant.White))
                direction++;
            else
                direction--;
            // lazy to filter if x - 1 == -1 and x + 1 == 8
            possibleMoves.Add(new int[2] { this.x + 1, this.y + direction });
            possibleMoves.Add(new int[2] { this.x - 1, this.y + direction });
            return possibleMoves;
        }
    }
}
