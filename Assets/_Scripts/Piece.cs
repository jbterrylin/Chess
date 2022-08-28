using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts
{
    public abstract class Piece
    {
        public GameObject obj = new();
        public int x;
        public int y;
        public int initX;
        public int initY;

        public void Instantiate(Texture2D texture, int y, int x)
        {
            this.initX = x;
            this.initY = y;
            this.x = x;
            this.y = y;

            // set SpriteRenderer
            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            renderer.sortingLayerName = Constant.ChessLayer;
            obj.name = texture.name;

            obj.transform.localScale = new Vector3(Constant.ScaleSize, Constant.ScaleSize, 1);

            var colli = obj.AddComponent<BoxCollider2D>();
            colli.size = new Vector2(0.25f, 0.25f);

            // add click event
            EventTrigger eventTrigger1 = obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnClick(); });
            eventTrigger1.triggers.Add(entry);

            // offset + (index * box width)
            obj.transform.position = new Vector3((Constant.SizeFor1Box / 4) + x * Constant.SizeFor1Box, (Constant.SizeFor1Box / 5) + y * Constant.SizeFor1Box, 0);
        }

        public void ChangePosition()
        {
            // offset + (index * box width)
            obj.transform.position = new Vector3((Constant.SizeFor1Box / 4) + x * Constant.SizeFor1Box, (Constant.SizeFor1Box / 5) + y * Constant.SizeFor1Box, 0);
        }

        public void ClearPiece()
        {
            if (obj.TryGetComponent<SpriteRenderer>(out _))
            {
                UnityEngine.Object.Destroy(obj);
                obj = null;
            }
        }

        public void OnClick()
        {
            switch (ChessBoard.GetInstance.CheckEventType(this))
            {
                case Constant.ShowPossible:
                    var possibleMoves = GetPossibleMove();
                    // if move king, need to filter suicide move
                    if (this.obj.name.Contains(Constant.King))
                    {
                        var enemyRulePos = PosCanSuicide();
                        possibleMoves = possibleMoves
                            .Where(pm => !enemyRulePos.Any(erp => erp[0] == pm[0] && erp[1] == pm[1]))
                            .ToList();
                    }
                    foreach (var possibleMove in possibleMoves)
                    {
                        // En passant
                        if (this.obj.name.Contains(Constant.Pawn) &&
                            this.x != possibleMove[0] &&
                            Util.GetPieceFromPieces(possibleMove[0], possibleMove[1]) == null)
                        {
                            ChessBoard.GetInstance.AddPossibleMove(possibleMove[0], possibleMove[1], Constant.EnPassant);
                        } 
                        else
                            ChessBoard.GetInstance.AddPossibleMove(possibleMove[0], possibleMove[1]);
                    }
                    break;
                case Constant.Nothing:
                    break;
            }
        }

        public abstract List<int[]> GetPossibleMove();

        // get the position that can get kill if enemy chess on there 
        // eg: all posible move even that position have ally and enemy
        // need to rewrite because GetPossibleMove not inlcude if
        //      chess A and B is enemy chess
        //      if chess A in chess B kill position
        //      because both of them in same team, so chess B cant move to chess A position, so that position will not in GetPossibleMove
        //      not in mean king can take chess A, but if take chess A = move to Chess B kill position, then cause error
        //      error is not hard error, mean possible move signal will show on chess A, but after click move to chess A position will get block by ChessBoard.CheckMoveValid
        // this is for IsMoveToSuicide
        public abstract List<int[]> GetKillPos();

        // for king move, prevent suicide move
        // actually can put function content to here, but i put in king class
        public virtual List<int[]> PosCanSuicide()
        {
            return null;
        }
    }
}
