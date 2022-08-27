﻿using System;
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
        public int y;
        public int x;

        public void Instantiate(Texture2D texture, int y, int x)
        {
            this.y = y;
            this.x = x;

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
            if (!obj.TryGetComponent<SpriteRenderer>(out _))
            {
                return;
            }
            
            switch (Chess_Board.GetInstance.CheckEventType(this))
            {
                case Constant.ShowPossible:
                    var possibleMoves = GetPossibleMove();
                    // if move king, need to filter suicide move
                    if (this.obj.name.Contains(Constant.King))
                    {
                        var enemyRulePos = IsMoveToSuicide();
                        possibleMoves = possibleMoves
                            .Where(pm => !enemyRulePos.Any(erp => erp[0] == pm[0] && erp[1] == pm[1]))
                            .ToList();
                    }
                    foreach (var possibleMove in possibleMoves)
                    {
                        Chess_Board.GetInstance.AddPossibleMove(possibleMove[0], possibleMove[1]);
                    }
                    break;
                case Constant.Nothing:
                    break;
            }
        }

        public abstract List<int[]> GetPossibleMove();

        // get the position that can get kill if enemy chess on there, basically only design for pawn or piece that move and kill is not in same space
        // this is for IsMoveToSuicide
        public virtual List<int[]> GetKillPos()
        {
            return GetPossibleMove();
        }

        // check move is check or not
        public abstract bool CheckCheck(int x, int y);

        // for king move, prevent suicide move
        public virtual List<int[]> IsMoveToSuicide()
        {
            return null;
        }
    }
}
