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
        public static Piece GetPieceFromPieces(int x, int y)
        {
            return Chess_Board.GetInstance.pieces.FirstOrDefault(p => p.y == y && p.x == x);
        }

        public static Piece GetPieceByObjName(string name)
        {
            return Chess_Board.GetInstance.pieces.FirstOrDefault(p => p.obj.name == name);
        }

        public static string IntToAlpha(int i)
        {
            bool isCaps = true;
            Char c = (Char)((isCaps ? 65 : 97) + (i - 1));
            return c.ToString();
        }
    }
}
