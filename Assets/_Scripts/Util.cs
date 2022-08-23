using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    class Util
    {
        public static Piece GetPieceFromPieces(int y, int x)
        {
            Debug.Log(Chess_Board.GetInstance.pieces.Count);
            Debug.Log(Chess_Board.GetInstance.pieces.FirstOrDefault(p => p.y == y && p.x == x) == null);
            return Chess_Board.GetInstance.pieces.FirstOrDefault(p => p.y == y && p.x == x);
        }
    }
}
