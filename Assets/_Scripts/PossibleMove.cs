using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts
{
    public class PossibleMove
    {
        public GameObject obj = new();
        public int[] pos = new int[2];

        public PossibleMove(int x, int y)
        {
            // set obj
            var objTexture = Resources.Load("possible_pos") as Texture2D;
            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(objTexture, new Rect(0, 0, objTexture.width, objTexture.height), Vector2.zero);
            renderer.sortingLayerName = Constant.PointerLayer;
            obj.transform.localScale = new Vector3(0.15f, 0.15f, 1);
            obj.transform.position = new Vector3((Constant.SizeFor1Box / 5) + x * Constant.SizeFor1Box, (Constant.SizeFor1Box / 5) + y * Constant.SizeFor1Box, 0);

            // set pos
            pos = new int[2] { x, y };

            // add click event
            var colli = obj.AddComponent<BoxCollider2D>();
            colli.size = new Vector2(9f, 9f);

            EventTrigger eventTrigger1 = obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnClick(); });
            eventTrigger1.triggers.Add(entry);
        }

        public void OnClick()
        {
            GameManager.Instance.moveToPos = new int[2] { pos[0], pos[1] };
            Chess_Board.GetInstance.MoveChess();
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(obj);
        }
    }
}
