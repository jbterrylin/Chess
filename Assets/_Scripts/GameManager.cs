using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Assets._Scripts;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public bool isWhiteTurn = true;
    public int[] chessSelectPos;
    public int[] moveToPos;
    public List<string> historyList = new();
    public GameObject history_scroll;

    void Start()
    {
        chessSelectPos = new int[2] { -1, -1 };
        moveToPos = new int[2] { -1, -1 };
        history_scroll = GameObject.Find("Canvas/History_Move/Viewport/Content/Text");

        _ = Chess_Board.GetInstance;
    }

    public void AddToHistoryMove(string text, string status)
    {
        Text myText = history_scroll.GetComponent<Text>();
        if (status == Constant.CheckMate)
        {
            myText.text = myText.text.Insert(myText.text.Length - 1, "#");
        } 
        else
        {
            Instance.historyList.Add(text);
            myText.text += "Move " + historyList.Count + "\n" + historyList.Last() + "\n";
        }
    }
}
