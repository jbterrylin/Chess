using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets._Scripts.Pieces
{
    class King : Piece
    {
        public override List<PossibleMove> GetPossibleMove()
        {
            List<PossibleMove> possibleMoves = new();
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
                        possibleMoves.Add(new PossibleMove( tmpx, tmpy ));
                    else if (Util.GetPieceFromPieces(tmpx, tmpy) != null &&
                        (isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.Black) ||
                        !isWhite && Util.GetPieceFromPieces(tmpx, tmpy).obj.name.Contains(Constant.White)))
                        possibleMoves.Add(new PossibleMove( tmpx, tmpy ));
                }
            }

            // TODO: castling
            foreach (var piece in ChessBoard.GetInstance.pieces)
            {
                if(isWhite && piece.obj.name.Contains(Constant.White + "_" + Constant.Rook) ||
                    !isWhite && piece.obj.name.Contains(Constant.Black + "_" + Constant.Rook))
                {
                    var rookName = isWhite ? Constant.White + "_" + Constant.Rook : Constant.Black + "_" + Constant.Rook;
                    if(GameManager.Instance.historyList.Select(history => this.obj.name).Count() == 0 && 
                        GameManager.Instance.historyList.Select(history => history.movePiece == rookName && history.oriX == this.initX && history.oriY == this.initY).Count() == 0)
                    {
                        int xi;
                        if(this.x > piece.x)
                            xi = 1;
                        else
                            xi = -1;
                        for(int i=this.x+xi; xi<piece.x; i+=xi)
                        {
                            if(Util.GetPieceFromPieces(i, this.y) != null)
                            {
                                return possibleMoves;
                            }
                        }

                        possibleMoves.Add(new PossibleMove( this.x + xi + xi, this.y, Constant.Castling ));
                    }
                }
            }

            return possibleMoves;
        }

        public override List<int[]> GetKillPos()
        {
            List<int[]> killPos = new();
            int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };

            for (int di = 0; di < dx.Length; di++)
            {
                var tmpx = this.x + dx[di];
                var tmpy = this.y + dy[di];

                if (tmpx >= 0 && tmpx < 8 && tmpy >= 0 && tmpy < 8)
                    killPos.Add(new int[2] { tmpx, tmpy });
            }

            return killPos;
        }

        public override List<int[]> PosCanSuicide()
        {
            var isWhite = this.obj.name.Contains(Constant.White);

            List<int[]> possibleMoves = new();
            foreach (var piece in ChessBoard.GetInstance.pieces)
                if (isWhite && piece.obj.name.Contains(Constant.Black) ||
                !isWhite && piece.obj.name.Contains(Constant.White))
                {
                    possibleMoves.AddRange(piece.GetKillPos());
                    //Debug.Log(piece.obj.name + " " + piece.x + "," + piece.y);
                    //foreach (var kp in piece.GetKillPos())
                    //{
                    //    Debug.Log(kp[0] + "," + kp[1]);
                    //}
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
