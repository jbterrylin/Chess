using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts
{
    public abstract class Skill
    {
        public int skillNo;
        public Texture2D texture;
        public String skillText;
        public Piece piece;
        public int cooldown;
        public int cd;
        public int x;
        public int y;
        public bool isWhite;

        public void ShowSkillToScene()
        {
            var img = GameObject.Find("Canvas/Skill_" + skillNo + "/Button").GetComponent<Image>();
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            var text = GameObject.Find("Canvas/Skill_" + skillNo + "/Text").GetComponent<Text>();
            text.text = skillText;

            EventTrigger eventTrigger1 = GameObject.Find("Canvas/Skill_" + skillNo + "/Button").AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { Skill1(); });
            eventTrigger1.triggers.Add(entry);
        }

        public abstract void Skill1();
    }
}
