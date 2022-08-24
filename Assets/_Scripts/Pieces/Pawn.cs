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
        public override void ShowPossibleMove()
        {
            int direction = 0;
            if (this.obj.name.Contains(Constant.White))
            {
                direction++;
            }
            else
            {
                direction--;
            }

            // charge
            if (
                ((this.obj.name.Contains(Constant.White) && this.y == 1) ||
                (this.obj.name.Contains(Constant.Black) && this.y == 6)) &&
                Util.GetPieceFromPieces(this.x, this.y + direction) == null &&
                Util.GetPieceFromPieces(this.x, this.y + direction + direction) == null
            )
            {
                Chess_Board.GetInstance.AddPossibleMove(this.x, this.y + direction + direction);
            }

            // normal move
            if (Util.GetPieceFromPieces(this.x, this.y + direction) == null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.x, this.y + direction);
            }

            // take piece
            // TODO: still can take own piece
            var isWhite = this.obj.name.Contains(Constant.White);
            if (Util.GetPieceFromPieces(this.x + 1, this.y + direction) != null)
            {
                if (isWhite && Util.GetPieceFromPieces(this.x + 1, this.y + direction).obj.name.Contains(Constant.Black) ||
                    !isWhite && Util.GetPieceFromPieces(this.x + 1, this.y + direction).obj.name.Contains(Constant.White))
                {
                    Chess_Board.GetInstance.AddPossibleMove(this.x + 1, this.y + direction);
                }
            }
            if (Util.GetPieceFromPieces(this.x - 1, this.y + direction) != null)
            {
                if (isWhite && Util.GetPieceFromPieces(this.x - 1, this.y + direction).obj.name.Contains(Constant.Black) ||
                    !isWhite && Util.GetPieceFromPieces(this.x - 1, this.y + direction).obj.name.Contains(Constant.White))
                {
                    Chess_Board.GetInstance.AddPossibleMove(this.x - 1, this.y + direction);
                }
            }

            // TODO: En passant
        }
    }
}
