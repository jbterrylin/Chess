using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Skills
{
    class Trench: Skill
    {
        public Trench(Piece piece)
        {
            this.piece = piece;
            var texture = Resources.Load("chess_new/white_pawn_showel") as Texture2D;
            this.skillNo = 1;
            this.texture = texture;
            this.skillText = "abc";
        }

        public override void Skill1()
        {
            Debug.Log("Skill1");
        }
    }
}
