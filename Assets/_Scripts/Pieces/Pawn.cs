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
            var cb = Chess_Board.GetInstance.pieces;

            // charge
            if (
                ((this.obj.name.Contains(Constant.White) && this.pos[0] == 1) ||
                (this.obj.name.Contains(Constant.Black) && this.pos[0] == 6)) &&
                cb[this.pos[0] + direction, this.pos[1]] == null &&
                cb[this.pos[0] + direction + direction, this.pos[1]] == null
            )
            {
                Chess_Board.GetInstance.AddPossibleMove(this.pos[0] + direction + direction, this.pos[1]);
            }

            // normal move
            if (cb[this.pos[0] + direction, this.pos[1]] == null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.pos[0] + direction, this.pos[1]);
            }

            // take piece
            if (this.pos[1] < 7 && cb[this.pos[0] + direction, this.pos[1] + 1] != null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.pos[0] + direction, this.pos[1] + 1);
            }
            if (this.pos[1] > 0 && cb[this.pos[0] + direction, this.pos[1] -1] != null)
            {
                Chess_Board.GetInstance.AddPossibleMove(this.pos[0] + direction, this.pos[1] - 1);
            }
        }
    }
}
