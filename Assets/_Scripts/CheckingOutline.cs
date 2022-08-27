using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class CheckingOutline
    {
        public GameObject obj = new();

        public CheckingOutline(int x, int y)
        {
            // set obj
            var objTexture = Resources.Load("checking_outline") as Texture2D;
            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(objTexture, new Rect(0, 0, objTexture.width, objTexture.height), Vector2.zero);
            renderer.sortingLayerName = Constant.ChessLayer;
            obj.transform.localScale = new Vector3(0.15f, 0.15f, 1);
            obj.transform.position = new Vector3(x * Constant.SizeFor1Box, y * Constant.SizeFor1Box, 0);
        }

        public void Destroy()
        {
            UnityEngine.Object.Destroy(obj);
        }
    }
}
