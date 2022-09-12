using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Scripts;
using Assets._Scripts.Pieces;
using Assets._Scripts.Pieces.NewPieces;
using System;
using System.Linq;
using Assets._Scripts.Skills;

public class ChessBoard
{
    private static ChessBoard instance = null;
    public static ChessBoard GetInstance
    {
        get
        {
            if (instance == null)
                instance = new ChessBoard();
            return instance;
        }
    }

    readonly int[,] chessBoard;
    public int[] chessSelectPos;
    public int[] moveToPos;

    public readonly List<Piece> pieces = new();
    readonly Dictionary<string, Texture2D[]> sprites = new();

    public GameObject selectedOutline;
    public List<CheckingOutline> checkingOutlineList = new();
    public List<PossibleMove> possiblePosList = new();


    // skill related
    public bool skillUsed = false;
    public List<Trench> trenchs = new();

    public ChessBoard()
    {
        chessBoard = Constant.ChessBoardNew;
        chessSelectPos = new int[2] { -1, -1 };
        moveToPos = new int[2] { -1, -1 };

        // get texture2D
        for (int chessType = 0; chessType < Constant.ChessTypes.Length; chessType++)
        {
            sprites[Constant.ChessTypes[chessType]] = new Texture2D[2];
            for (int chessColor = 0; chessColor < Constant.ChessColors.Length; chessColor++)
            {
                var texture = Resources.Load("chess_green/" + Constant.ChessColors[chessColor] + "_" + Constant.ChessTypes[chessType]) as Texture2D;
                if(texture == null)
                {
                    texture = Resources.Load("chess_new/" + Constant.ChessColors[chessColor] + "_" + Constant.ChessTypes[chessType]) as Texture2D;
                }
                sprites[Constant.ChessTypes[chessType]][chessColor] = texture;
                texture.name = Constant.ChessColors[chessColor] + "_" + Constant.ChessTypes[chessType];
            }
        }

        // set texture2D to scene
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                switch (chessBoard[y, x])
                {
                    case 1:
                        pieces.Add(new Pawn());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.Pawn][0], y, x);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.Pawn][1], y, x);
                        }
                        break;
                    case 2:
                        pieces.Add(new Rook());
                        if (y <2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.Rook][0], y, x);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.Rook][1], y, x);
                        }
                        break;
                    case 3:
                        pieces.Add(new Knight());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.Knight][0], y, x);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.Knight][1], y, x);
                        }
                        break;
                    case 4:
                        pieces.Add(new Bishop());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.Bishop][0], y, x);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.Bishop][1], y, x);
                        }
                        break;
                    case 5:
                        pieces.Add(new Queen());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.Queen][0], y, x);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.Queen][1], y, x);
                        }
                        break;
                    case 6:
                        pieces.Add(new King());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.King][0], y, x);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.King][1], y ,x);
                        }
                        break;
                    case 7:
                        pieces.Add(new PawnShowel());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.PawnShowel][0], y, x, true);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.PawnShowel][1], y, x, true);
                        }
                        break;
                    case 8:
                        pieces.Add(new KnightTroops());
                        if (y < 2)
                        {
                            pieces.Last().Instantiate(sprites[Constant.KnightTroops][0], y, x, true);
                        }
                        else
                        {
                            pieces.Last().Instantiate(sprites[Constant.KnightTroops][1], y, x, true);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // set selectedOutline
        {
            selectedOutline = new();
            var selectedOutlineTexture = Resources.Load("selected_outline") as Texture2D;
            SpriteRenderer renderer = selectedOutline.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(selectedOutlineTexture, new Rect(0, 0, selectedOutlineTexture.width, selectedOutlineTexture.height), Vector2.zero);
            renderer.sortingLayerName = Constant.ChessLayer;
            selectedOutline.transform.localScale = new Vector3(0.15f, 0.15f, 1);
            selectedOutline.SetActive(false);
        }
    }

    public string CheckEventType(Piece piece)
    {
        selectedOutline.SetActive(true);
        selectedOutline.transform.position = new Vector3(piece.x * Constant.SizeFor1Box, piece.y * Constant.SizeFor1Box, 0);

        GameManager.Instance.ClearSkillList();
        ClearPossibleMove();

        if (
            (piece.obj.name.Contains(Constant.White) &&
            GameManager.Instance.isWhiteTurn) ||
            (piece.obj.name.Contains(Constant.Black) &&
            !GameManager.Instance.isWhiteTurn)
        )
        {
            chessSelectPos = new int[2] { piece.x, piece.y };
            piece.ShowSkills();
            return Constant.ShowPossible;
        } 
        return Constant.Nothing;
    }

    public void MoveChess(PossibleMove possibleMove)
    {
        var moveType = possibleMove.moveType;

        var oriX = chessSelectPos[0];
        var oriY = chessSelectPos[1];
        var newX = moveToPos[0];
        var newY = moveToPos[1];

        // remove and add back is not meaningless, if not cant detect player take the piece that check their king
        var oldPiece = Util.GetPieceFromPieces(newX, newY);
        pieces.Remove(oldPiece);

        Piece killPawn = null;
        if (moveType == Constant.EnPassant)
        {
            killPawn = Util.GetPieceFromPieces(newX, newY + (newY > oriY ? -1 : 1));
            pieces.Remove(killPawn);
        }

        Piece castleRook = null;
        if (moveType == Constant.Castling)
        {
            castleRook = Util.GetPieceFromPieces(possibleMove.castleRookPos[0], possibleMove.castleRookPos[1]);
            castleRook.x = (newX > oriX ? (newX-1) : (newX+1));
        }

        var movedPiece = Util.GetPieceFromPieces(oriX, oriY);
        movedPiece.x = newX;
        movedPiece.y = newY;

        if (!CheckMoveValid())
        {
            if (oldPiece != null)
                pieces.Add(oldPiece);
            if (killPawn != null)
                pieces.Add(killPawn);
            if (castleRook != null)
                castleRook.x = possibleMove.castleRookPos[0];

            movedPiece.x = oriX;
            movedPiece.y = oriY;
            moveToPos = new int[2] { -1, -1 };

            return;
        }

        var killedPieceName = (oldPiece == null ? killPawn == null ? "" : killPawn.obj.name : oldPiece.obj.name);

        oldPiece?.ClearPiece();
        if(moveType == Constant.EnPassant)
            killPawn?.ClearPiece();
        if (moveType == Constant.Castling)
            castleRook.ChangePosition();
        movedPiece.ChangePosition();

        Reset();

        // add history
        if (moveType == Constant.Castling)
            GameManager.Instance.AddToHistoryMove(new History(movedPiece.obj.name, oriX, oriY, newX, newY, killedPieceName), Constant.Castling);
        else
            GameManager.Instance.AddToHistoryMove(new History(movedPiece.obj.name, oriX, oriY, newX, newY, killedPieceName), Constant.Move);
        GameManager.Instance.AddToHistoryMove(null, GetMoveStatus());

        GameManager.Instance.isWhiteTurn = !GameManager.Instance.isWhiteTurn;
    }

    public void Reset()
    {
        // reset move related var and obj
        chessSelectPos = new int[2] { -1, -1 };
        moveToPos = new int[2] { -1, -1 };
        skillUsed = false;
        ClearPossibleMove();
        if (checkingOutlineList.Count != 0)
            foreach (var checkingOutline in checkingOutlineList)
            {
                checkingOutline.Destroy();
            }
        checkingOutlineList.Clear();
        selectedOutline.SetActive(false);
        GameManager.Instance.ClearSkillList();
    }

    // check all enemy piece can take king if chess moved
    public bool CheckMoveValid()
    {
        var moveValid = true;
        Piece king = (GameManager.Instance.isWhiteTurn ? Util.GetPieceByObjName(Constant.White + "_" + Constant.King) : Util.GetPieceByObjName(Constant.Black + "_" + Constant.King));
        foreach(var piece in pieces)
        {
            if (GameManager.Instance.isWhiteTurn == piece.obj.name.Contains(Constant.Black) ||
                !GameManager.Instance.isWhiteTurn == piece.obj.name.Contains(Constant.White))
                if (piece.GetPossibleMove().Where(kp => kp.x == king.x && kp.y == king.y).Count() > 0)
                {
                    moveValid = false;
                    checkingOutlineList.Add(new CheckingOutline(piece.x, piece.y));
                }
        }
        return moveValid;
    }


    // check enemy king get checkmated or not, also return status where Check, CheckMate, or juz normal move
    public string GetMoveStatus()
    {
        Piece king = (GameManager.Instance.isWhiteTurn ? Util.GetPieceByObjName(Constant.Black + "_" + Constant.King) : Util.GetPieceByObjName(Constant.White + "_" + Constant.King));
        var possibleMoves = king.GetPossibleMove();
        var enemyRulePos = king.PosCanSuicide();

        // check enemy king is in check or not
        if (enemyRulePos.Any(erp => erp[0] == king.x && erp[1] == king.y))
        {
            possibleMoves = possibleMoves
                .Where(pm => !enemyRulePos.Any(erp => erp[0] == pm.x && erp[1] == pm.y))
                .ToList();
            // if king is no where to move
            if (possibleMoves.Count == 0)
            {
                List<int[]> checkingPiecePos = new();
                foreach (var piece in pieces)
                {
                    if (GameManager.Instance.isWhiteTurn == piece.obj.name.Contains(Constant.White) ||
                        !GameManager.Instance.isWhiteTurn == piece.obj.name.Contains(Constant.Black))
                        if (piece.GetPossibleMove().Where(kp => kp.x == king.x && kp.y == king.y).Count() > 0)
                        {
                            checkingPiecePos.Add(new int[2] { piece.x, piece.y });
                        }
                }
                // check how many piece is checking the king, if > 1 sure checkmate,
                // if = 1, check that checking piece can take by other piece or not
                // GetPossibleMove already prevent king take the checking piece that protect by his ally
                if (checkingPiecePos.Count > 1)
                {
                    return Constant.CheckMate;
                }
                else if (checkingPiecePos.Count == 1)
                {
                    foreach (var piece in pieces)
                    {
                        if (GameManager.Instance.isWhiteTurn == piece.obj.name.Contains(Constant.Black) ||
                            !GameManager.Instance.isWhiteTurn == piece.obj.name.Contains(Constant.White))
                            if (!(piece.obj.name.Contains(Constant.King)) && piece.GetPossibleMove().Where(kp => kp.x == checkingPiecePos[0][0] && kp.y == checkingPiecePos[0][1]).Count() > 0)
                            {
                                return Constant.Check;
                            }
                    }
                    return Constant.CheckMate;
                }
            }
            return Constant.Check;
        }

        return Constant.Move;
    }

    public void AddPossibleMove(PossibleMove possibleMove)
    {
        possiblePosList.Add(possibleMove);
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
