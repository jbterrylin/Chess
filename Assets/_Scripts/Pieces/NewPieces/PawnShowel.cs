using Assets._Scripts.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Pieces.NewPieces
{
    class PawnShowel : Piece
    {
        public override List<PossibleMove> GetPossibleMove()
        {
            List<PossibleMove> possibleMoves = new();
            int direction = this.obj.name.Contains(Constant.White) ? 1 : -1;

            // normal move
            if (!Util.CheckEnemyTrenchExist(this.x, this.y + direction, this.obj.name.Contains(Constant.White)) && 
                Util.GetPieceFromPieces(this.x, this.y + direction) == null)
            {
                possibleMoves.Add(new PossibleMove(this.x, this.y + direction));

                // charge
                if (!Util.CheckEnemyTrenchExist(this.x, this.y + direction + direction, this.obj.name.Contains(Constant.White)) &&
                    !Util.CheckTrenchExist(this.x, this.y + direction) &&
                    ((this.obj.name.Contains(Constant.White) && this.y == 1) ||
                    (this.obj.name.Contains(Constant.Black) && this.y == 6)) &&
                    Util.GetPieceFromPieces(this.x, this.y + direction + direction) == null
                )
                {
                    possibleMoves.Add(new PossibleMove(this.x, this.y + direction + direction));
                }
            }

            var dxs = new int[2] { -1, +1 };

            var isWhite = this.obj.name.Contains(Constant.White);
            // take piece
            foreach (int dx in dxs)
            {
                if (Util.GetPieceFromPieces(this.x + dx, this.y + direction) != null)
                {
                    if (isWhite && Util.GetPieceFromPieces(this.x + dx, this.y + direction).obj.name.Contains(Constant.Black) ||
                        !isWhite && Util.GetPieceFromPieces(this.x + dx, this.y + direction).obj.name.Contains(Constant.White))
                    {
                        possibleMoves.Add(new PossibleMove(this.x + dx, this.y + direction));
                    }
                }
            }

            // En passant
            if (GameManager.Instance.historyList.Count > 0)
            {
                var lastMove = GameManager.Instance.historyList.Last();
                foreach (int dx in dxs)
                {
                    if (Util.GetPieceFromPieces(this.x + dx, this.y) != null)
                    {
                        if (isWhite && Util.GetPieceFromPieces(this.x + dx, this.y).obj.name.Contains(Constant.Black + "_" + Constant.Pawn) ||
                            !isWhite && Util.GetPieceFromPieces(this.x + dx, this.y).obj.name.Contains(Constant.White + "_" + Constant.Pawn))
                        {
                            if (lastMove.newX == this.x + dx && lastMove.newY == this.y && 
                                lastMove.oriY == this.y + direction + direction && 
                                lastMove.movePiece.Contains(Constant.Pawn))
                                if (!Util.CheckEnemyTrenchExist(this.x + dx, this.y + direction, isWhite))
                                    possibleMoves.Add(new PossibleMove(this.x + dx, this.y + direction, Constant.EnPassant));
                        }
                    }
                }
            }

            return possibleMoves;
        }

        public override List<int[]> GetKillPos()
        {
            List<int[]> possibleMoves = new();
            int direction = this.obj.name.Contains(Constant.White) ? 1 : -1;
            // lazy to filter if x - 1 == -1 and x + 1 == 8
            possibleMoves.Add(new int[2] { this.x + 1, this.y + direction });
            possibleMoves.Add(new int[2] { this.x - 1, this.y + direction });
            return possibleMoves;
        }

        public override void InstantiateSkill()
        {
            skills.Add(new Trench(this));
        }
    }
}
