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
        public int[] pos = new int[2];

        public void SetSprite(Texture2D texture)
        {
            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            renderer.sortingLayerName = "Chess";
            obj.name = texture.name;

            obj.transform.localScale = new Vector3(Constant.ScaleSize, Constant.ScaleSize, 1);
        }

        public void Instantiate(int y, int x)
        {
            // offset + (index * box width)
            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                obj.transform.position = new Vector3((Constant.SizeFor1Box / 4) + x * Constant.SizeFor1Box, (Constant.SizeFor1Box / 5) + y * Constant.SizeFor1Box, 0);
            }
            var colli = obj.AddComponent<BoxCollider2D>();
            colli.size = new Vector2(0.25f, 0.25f);

            EventTrigger eventTrigger1 = obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnClick(); });
            eventTrigger1.triggers.Add(entry);
            pos = new int[2] { y, x };
        }
        public void OnClick()
        {
            switch (Chess_Board.GetInstance.CheckEventType(this))
            {
                case Constant.ShowPossible:
                    ShowPossibleMove();
                    break;
                case Constant.CheckValid:
                    CheckValid();
                    break;
                case Constant.Nothing:
                    break;
            }
        }

        public abstract void ShowPossibleMove();
        public abstract void CheckValid();
    }
}
