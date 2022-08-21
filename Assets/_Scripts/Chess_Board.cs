using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Scripts;
using Assets._Scripts.Pieces;
using System;

public class Chess_Board
{
    private static Chess_Board instance = null;
    public static Chess_Board GetInstance
    {
        get
        {
            if (instance == null)
                instance = new Chess_Board();
            return instance;
        }
    }

    public readonly Piece[,] pieces = new Piece[8, 8];
    readonly Dictionary<string, Texture2D[]> sprites = new();
    public GameManager gm;

    public GameObject selectedOutline;
    public List<PossibleMove> possiblePosList = new();

    public Chess_Board()
    {
        // get texture2D
        for (int chessType = 0; chessType < Constant.ChessTypes.Length; chessType++)
        {
            sprites[Constant.ChessTypes[chessType]] = new Texture2D[2];
            for (int chessColor = 0; chessColor < Constant.ChessColors.Length; chessColor++)
            {
                var texture = Resources.Load("chess_green/" + Constant.ChessColors[chessColor] + "_" + Constant.ChessTypes[chessType]) as Texture2D;
                sprites[Constant.ChessTypes[chessType]][chessColor] = texture;
                texture.name = Constant.ChessColors[chessColor] + "_" + Constant.ChessTypes[chessType];
            }
        }

        // set texture2D to scene
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                switch (Constant.ChessBoardInit[y, x])
                {
                    case 1:
                        pieces[y, x] = new Pawn();
                        if (y < 2)
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Pawn][0]);
                        }
                        else
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Pawn][1]);
                        }
                        break;
                    case 2:
                        pieces[y, x] = new Rook();
                        if (y <2)
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Rook][0]);
                        }
                        else
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Rook][1]);
                        }
                        break;
                    case 3:
                        pieces[y, x] = new Knight();
                        if (y < 2)
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Knight][0]);
                        }
                        else
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Knight][1]);
                        }
                        break;
                    case 4:
                        pieces[y, x] = new Bishop();
                        if (y < 2)
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Bishop][0]);
                        }
                        else
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Bishop][1]);
                        }
                        break;
                    case 5:
                        pieces[y, x] = new Queen();
                        if (y < 2)
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Queen][0]);
                        }
                        else
                        {
                            pieces[y, x].SetSprite(sprites[Constant.Queen][1]);
                        }
                        break;
                    case 6:
                        pieces[y, x] = new King();
                        if (y < 2)
                        {
                            pieces[y, x].SetSprite(sprites[Constant.King][0]);
                        }
                        else
                        {
                            pieces[y, x].SetSprite(sprites[Constant.King][1]);
                        }
                        break;
                    default:
                        break;

                }
                switch (Constant.ChessBoardInit[y, x])
                {
                    case > 0:
                        pieces[y, x].Instantiate(y, x);
                        break;
                }

                //board[x, y] = blackSprite;

                //Vector3 position = new Vector3(10, 5, 0);

                //board[x, y].transform.position = position;
                //Instantiate(board[x, y], new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

    public string CheckEventType(Piece piece)
    {
        // set selectedOutline
        if (selectedOutline == null)
        {
            selectedOutline = new();
            var selectedOutlineTexture = Resources.Load("selected_outline") as Texture2D;
            SpriteRenderer renderer = selectedOutline.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(selectedOutlineTexture, new Rect(0, 0, selectedOutlineTexture.width, selectedOutlineTexture.height), Vector2.zero);
            renderer.sortingLayerName = "Chess";
            selectedOutline.transform.localScale = new Vector3(0.15f, 0.15f, 1);
        }
        selectedOutline.transform.position = new Vector3(piece.pos[1] * Constant.SizeFor1Box, piece.pos[0] * Constant.SizeFor1Box, 0);

        ClearPossibleMove();

        if (
            (piece.obj.name.Contains(Constant.White) &&
            GameManager.GetInstance.isWhiteTurn) ||
            (piece.obj.name.Contains(Constant.Black) &&
            !GameManager.GetInstance.isWhiteTurn)
        )
        {
            GameManager.GetInstance.chessSelectPos = new int[2] { piece.pos[0], piece.pos[1] };
            return Constant.ShowPossible;
        } 
        return Constant.Nothing;
    }

    public void MoveChess()
    {
        Debug.Log("MoveChess");
        GameManager.GetInstance.chessSelectPos = new int[2] { -1, -1 };
        GameManager.GetInstance.moveToPos = new int[2] { -1, -1 };
    }

    public void AddPossibleMove(int y, int x)
    {
        possiblePosList.Add(new PossibleMove(y, x));
    }

    public void ClearPossibleMove()
    {
        if (possiblePosList.Count == 0)
            return;
        foreach (var possiblePos in possiblePosList)
        {
            possiblePos.Destroy();
        }
        possiblePosList.Clear();
    }
}
