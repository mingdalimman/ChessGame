using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChessPieceBehavior : MonoBehaviour
{
    public int startPosX;
    public int startPosY;
    public enum chessPieceType { pawn, rook, knight, bishop, queen, king };
    public enum chessPieceColor { White, Black };
    public chessPieceType chessPiece;
    public chessPieceColor chessColor;

    public List<int[]> legalMoves;

    public GameObject highlightEffectPrefab;
    List<GameObject> highlights;

    private bool isFirstTurn = true;

    GameObject currentChessSquare;

    bool captured = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        highlights = new List<GameObject>();

        legalMoves = new List<int[]>();
        SetStartPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool MovePiece(Vector3 squarepos)
    {
        print("clicked on " + squarepos.x + " " + squarepos.z);

        foreach (int[] movePos in legalMoves)
        {
            print("searching " + movePos[0] + " " + movePos[1]);

            if (squarepos.x == movePos[0] && squarepos.z == movePos[1])
            {
                transform.position = new Vector3(squarepos.x, 1f, squarepos.z);
                isFirstTurn = false;
                currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[] { (int)squarepos.x, (int)squarepos.z });
                GameObject.FindObjectOfType<Board_Manager>().SwitchTurn();
                ResetLegalMoves();
                return true;
            }
        }
        print("Apologies, IDIOT, but that is not a legal move and unless you intend to go to the chess courts, please choose an actually legal move, IDIOT.");

        return false;

    }

    void SetStartPos()
    {
        currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[] { startPosX, startPosY });
        transform.position = new Vector3(currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[0], 1f, currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[1]);
    }

    void ResetLegalMoves()
    {
        foreach (ChessPieceBehavior chessPiece in FindObjectsOfType<ChessPieceBehavior>())
        {
            chessPiece.legalMoves.Clear();
            chessPiece.HighlightLegalMoves();
        }
    }

    void SetLegalMoves()
    {
        ChessPieceBehavior[] ChessPieces = GameObject.FindObjectsOfType<ChessPieceBehavior>();
        int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;

        ChessPieceBehavior[] allChessPieces = GameObject.FindObjectsOfType<ChessPieceBehavior>();

        ResetLegalMoves();

        int back = -1;
        int left = -1;

        int front = 1;
        int right = 1;

        int center = 0;

        if (chessPiece == chessPieceType.pawn)
        {
            if (chessColor == chessPieceColor.White)
            {
                if (isFirstTurn)
                {
                    legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 1 });
                    legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 2 });
                }
                else
                {
                    legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 1 });
                }
            }
            else
            {
                if (isFirstTurn)
                {
                    legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] - 1 });
                    legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] - 2 });
                }
                else
                {
                    legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] - 1 });
                }
            }
        }
        if (chessPiece == chessPieceType.bishop)
        {
            CheckLegalMovesDirection(right, front, ChessPieces);
            CheckLegalMovesDirection(right, back, ChessPieces);
            CheckLegalMovesDirection(left, front, ChessPieces);
            CheckLegalMovesDirection(left, back, ChessPieces);

            print("Shetting Bhishop Mhoves");
            print("mooooooooooooooo I want fooooooooooooooood and legooooooooo merch and a pet pigggggggyyyyyy");
        }

        if (chessPiece == chessPieceType.queen)
        {
            CheckLegalMovesDirection(right, front, ChessPieces);
            CheckLegalMovesDirection(right, back, ChessPieces);
            CheckLegalMovesDirection(left, front, ChessPieces);
            CheckLegalMovesDirection(left, back, ChessPieces);
            CheckLegalMovesDirection(center, front, ChessPieces);
            CheckLegalMovesDirection(center, back, ChessPieces);
            CheckLegalMovesDirection(right, center, ChessPieces);
            CheckLegalMovesDirection(left, center, ChessPieces);
        }

        if (chessPiece == chessPieceType.king)
        {
            // print(currentChessSquare.name);
            {
                legalMoves.Add(new int[] { currentSquarePos[0] + 1, currentSquarePos[1] + 1 });
                legalMoves.Add(new int[] { currentSquarePos[0] - 1, currentSquarePos[1] - 1 });
                legalMoves.Add(new int[] { currentSquarePos[0] + 1, currentSquarePos[1] - 1 });
                legalMoves.Add(new int[] { currentSquarePos[0] - 1, currentSquarePos[1] + 1 });

                legalMoves.Add(new int[] { currentSquarePos[0] + 1, currentSquarePos[1] });
                legalMoves.Add(new int[] { currentSquarePos[0] - 1, currentSquarePos[1] });
                legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 1 });
                legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] - 1 });
            }
        }

        if (chessPiece == chessPieceType.rook)
        {
            CheckLegalMovesDirection(center, front, ChessPieces);
            CheckLegalMovesDirection(center, back, ChessPieces);
            CheckLegalMovesDirection(right, center, ChessPieces);
            CheckLegalMovesDirection(left, center, ChessPieces);
        }

        if (chessPiece == chessPieceType.knight)
        {
            // print(currentChessSquare.name);
            legalMoves.Add(new int[] { currentSquarePos[0] + 1, currentSquarePos[1] + 2 });
            legalMoves.Add(new int[] { currentSquarePos[0] - 1, currentSquarePos[1] + 2 });
            legalMoves.Add(new int[] { currentSquarePos[0] + 1, currentSquarePos[1] + -2 });
            legalMoves.Add(new int[] { currentSquarePos[0] - 1, currentSquarePos[1] + -2 });
            legalMoves.Add(new int[] { currentSquarePos[0] + 2, currentSquarePos[1] + 1 });
            legalMoves.Add(new int[] { currentSquarePos[0] + 2, currentSquarePos[1] - 1 });
            legalMoves.Add(new int[] { currentSquarePos[0] - 2, currentSquarePos[1] + 1 });
            legalMoves.Add(new int[] { currentSquarePos[0] - 2, currentSquarePos[1] - 1 });

        }

        HighlightLegalMoves();
    }



    void HighlightLegalMoves()
    {
        foreach (GameObject highlight in highlights)
        {
            Destroy(highlight);
        }
        highlights.Clear();
        foreach (int[] movePos in legalMoves)
        {
            highlights.Add(Instantiate(highlightEffectPrefab, new Vector3(movePos[0], 1, movePos[1]), Quaternion.identity));
        }
    }

    private void OnMouseDown()
    {
        if (!captured)
        {
            if (chessColor == GameObject.FindObjectOfType<Board_Manager>().currentTurnColor)
            {
                GameObject.FindObjectOfType<Board_Manager>().selectedChessPiece = this;
                SetLegalMoves();
            }
            else
            {
                captured = false;
                captured = GameObject.FindObjectOfType<Board_Manager>().selectedChessPiece.MovePiece(new Vector3(currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[0], 1f, currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[1]));

                if (captured)
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(6, 20), Random.Range(12, 16), Random.Range(6, 20)));

                    StartCoroutine("DestroyPiece");
                }

                //GameObject.FindObjectOfType<Board_Manager>().selectedChessPiece.MovePiece(new Vector3(currentChessSquare.GetComponent<Chess_Square_Info>))
                //Destroy(gameObject);
            }
        }


    }

    bool CheckBlocked(int[] pos, ChessPieceBehavior[] pieces, out chessPieceColor color)
    {
        foreach (ChessPieceBehavior piece in pieces)
        {
            if (pos[0] == piece.currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[0])
            {
                if (pos[1] == piece.currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[1])
                {
                    color = piece.chessColor;
                    return true;
                }
            }
        }
        color = chessPieceColor.Black;
        return false;
    }

    void CheckLegalMovesDirection(int directionX, int directionY, ChessPieceBehavior[] pieces)
    {
        int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
        for (int step = 1; step < 8; step++)
        {
            int offsetX = step * directionX;
            int offsetY = step * directionY;

            chessPieceColor colorChecked;
            if (CheckBlocked(new int[] { currentSquarePos[0] + offsetX, currentSquarePos[1] + offsetY }, pieces, out colorChecked))
            {

                if (colorChecked != chessColor)
                {
                    legalMoves.Add(new int[] { currentSquarePos[0] + offsetX, currentSquarePos[1] + offsetY });
                }
                break;
            }
            else
            {
                legalMoves.Add(new int[] { currentSquarePos[0] + offsetX, currentSquarePos[1] + offsetY });
            }
        }
    }

    IEnumerator DestroyPiece()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
