using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts
{
    static class Constant
    {
        public static readonly int[,] ChessBoardInit = { 
            { 2,3,4,5,6,4,3,2 }, // white
            { 1,1,1,1,1,1,1,1 }, // white
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 1,1,1,1,1,1,1,1 }, // black
            { 2,3,4,5,6,4,3,2 }, // black
        };

        public static readonly string EnPassant = "EnPassant";
        public static readonly string Castling = "Castling";

        public static readonly string Move = "Move";
        public static readonly string Check = "Check";
        public static readonly string CheckMate = "CheckMate";

        public static readonly string White = "white";
        public static readonly string Black = "black";

        public static readonly string[] ChessColors =
        {
            White,
            Black,
        };

        public static readonly string Pawn = "pawn";
        public static readonly string Rook = "rook";
        public static readonly string Knight = "knight";
        public static readonly string Bishop = "bishop";
        public static readonly string Queen = "queen";
        public static readonly string King = "king";

        public static readonly string[] ChessTypes =
        {
            Pawn,
            Rook,
            Knight,
            Bishop,
            Queen,
            King,
        };

        public static readonly float Pos0InX = 0.7031f;
        public static readonly float Pos0InY = 0.7031f;
        public static readonly float SizeFor1Box = 1.407213f;
        public static readonly float ScaleSize = 5;

        public const string ShowPossible = "show";
        public const string CheckValid = "check";
        public const string Nothing = "";

        public const string ChessLayer = "Chess";    //LOW
        public const string PointerLayer = "Pointer";       //HIGH
    }
}
