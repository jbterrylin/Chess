using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Skills
{
    public class Trench: Skill
    {
        GameObject obj = new();

        public Trench(Piece piece)
        {
            this.piece = piece;
            var texture = Resources.Load("chess_new/white_pawn_showel") as Texture2D;
            this.skillNo = 1;
            this.texture = texture;
            this.skillText = "abc";
            this.cooldown = 0;
        }

        public override void Skill1()
        {
            if(ChessBoard.GetInstance.skillUsed)
            {
                Debug.Log("skillUsed");
                return;
            }

            var texture = Resources.Load("skills/trench") as Texture2D;

            int direction = piece.obj.name.Contains(Constant.White) ? 1 : -1;

            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sortingLayerName = Constant.ChessLayer;
            renderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            
            obj.transform.localScale = new Vector3(Constant.ScaleSize, Constant.ScaleSize, 1);
            obj.transform.position = new Vector3((Constant.SizeFor1Box / 5) + piece.x * Constant.SizeFor1Box, (Constant.SizeFor1Box / 2.5f) + (piece.y + direction) * Constant.SizeFor1Box, 0);
            obj.name = (obj.name.Contains(Constant.White) ? Constant.White : Constant.Black) + "trench";

            x = piece.x;
            y = piece.y + direction;
            isWhite = piece.obj.name.Contains(Constant.White);


            ChessBoard.GetInstance.skillUsed = true;
            ChessBoard.GetInstance.ClearPossibleMove();
            ChessBoard.GetInstance.trenchs.Add(this);
        }

        public void RemoveTrenchFromScene()
        {
            UnityEngine.Object.Destroy(obj);
        }
    }
}
