using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;

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

    void Start()
    {
        chessSelectPos = new int[2] { -1, -1 };
        moveToPos = new int[2] { -1, -1 };

        _ = Chess_Board.GetInstance;
    }

    
}
