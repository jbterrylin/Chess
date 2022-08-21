using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new();
                instance = go.AddComponent<GameManager>();  
            }
            return instance;
        }
    }

    private Chess_Board board;
    public bool isWhiteTurn = true;
    public int[] chessSelectPos;
    public int[] moveToPos;

    void Start()
    {
        chessSelectPos = new int[2] { -1, -1 };
        moveToPos = new int[2] { -1, -1 };

        board = new Chess_Board();
    }

    
}
