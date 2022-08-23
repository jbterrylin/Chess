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
                Util.GetPieceFromPieces(this.y + direction, this.x) == null &&
                Util.GetPieceFromPieces(this.y + direction + direction, this.x) == null
            )
            {
                Chess_Board.GetInstance.AddPossibleMove(this.y + direction + direction, this.x);
            }

            // normal move
            if (Util.GetPieceFromPieces(this.y + direction, this.x) == null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.y + direction, this.x);
            }

            // take piece
            // TODO: still can take own piece
            if (this.x < 7 && Util.GetPieceFromPieces(this.y + direction, this.x + 1) != null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.y + direction, this.x + 1);
            }
            if (this.x > 0 && Util.GetPieceFromPieces(this.y + direction, this.x - 1) != null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.y + direction, this.x - 1);
            }
        }
    }
}
