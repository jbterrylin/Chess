﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets._Scripts.Pieces
{
    class King : Piece
    {
        public override List<int[]> GetPossibleMove()
        {
            List<int[]> possibleMoves = new();
            var isWhite = this.obj.name.Contains(Constant.White);
            int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };

            for (int di = 0; di < dx.Length; di++)
            {
                var tmpx = this.x + dx[di];
                var tmpy = this.y + dy[di];

                if (tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                {
                    if (Util.GetPieceFromPieces(tmpx, tmpy) == null)
                        possibleMoves.Add(new int[2] { tmpx, tmpy });
                    else if (Util.GetPieceFromPieces(tmpx, tmpy) != null &&
                        (isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black) ||
                        !isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White)))
                        possibleMoves.Add(new int[2] { tmpx, tmpy });
                }
            }


            return possibleMoves;
            
        }

        public override bool CheckCheck(int x, int y)
        {
            return false;
        }

        public override List<int[]> IsMoveToSuicide()
        {
            var isWhite = this.obj.name.Contains(Constant.White);

            List<int[]> possibleMoves = new();
            foreach (var piece in Chess_Board.GetInstance.pieces)
                if (isWhite && piece.obj.name.Contains(Constant.Black) ||
                !isWhite && piece.obj.name.Contains(Constant.White))
                {
                    possibleMoves.AddRange(piece.GetKillMove());
                    possibleMoves = possibleMoves.
                        Select(e =>
                        new
                        {
                            x = e[0],
                            y = e[1],
                        }).
                        Distinct().
                        Select(e => new int[2] { e.x, e.y }).
                        ToList();
                }

            return possibleMoves;
        }
    }
}
