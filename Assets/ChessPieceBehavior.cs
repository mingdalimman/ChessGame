using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChessPieceBehavior : MonoBehaviour
{
    public int startPosX;
    public int startPosY;
    public enum chessPieceType { pawn, rook, knight, bishop, queen, king };
    public enum chessPieceColor { white, black };
    public chessPieceType chessPiece;
    public chessPieceColor chessColor;

    public List<int[]> legalMoves;

    public GameObject highlightEffectPrefab;
        List<GameObject> highlights;

    GameObject currentChessSquare;

    // Start is called before the first frame update
    void Start()
    {
        highlights = new List<GameObject>();

        legalMoves = new List<int[]>();
        SetStartPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePiece(Vector3 squarepos)
    {
        print("clicked on " + squarepos.x + " " + squarepos.z);

        foreach (int[] movePos in legalMoves)
        {
            print("searching " + movePos[0] + " " + movePos[1]);

            if (squarepos.x == movePos[0] && squarepos.z == movePos[1])
            {
                transform.position = new Vector3(squarepos.x, 1f, squarepos.z);
                currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[] { (int)squarepos.x, (int)squarepos.z });
                SetLegalMoves();
                return;
            }
        }
        print("Apologies, IDIOT, but that is not a legal move and unless you intend to go to the chess courts, please choose an actually legal move, IDIOT.");
    }

    void SetStartPos()
    {
        currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[] { startPosX, startPosY });
        transform.position = new Vector3(currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[0], 1f, currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[1]);
    }

    void SetLegalMoves()
    {
        foreach (ChessPieceBehavior chessPiece in FindObjectsOfType<ChessPieceBehavior>())
        {
            chessPiece.legalMoves.Clear();
            chessPiece.HighlightLegalMoves();
        }

        if (chessPiece == chessPieceType.pawn)
        {
            // print(currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
            legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 1 });
            legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 2 });
        }
        if (chessPiece == chessPieceType.bishop)
        {
            print("Shetting Bhishop Mhoves");//currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
            for (int diagonal = 0; diagonal < 8; diagonal++)
            {
                legalMoves.Add(new int[] { currentSquarePos[0] + diagonal, currentSquarePos[1] + diagonal });
                legalMoves.Add(new int[] { currentSquarePos[0] - diagonal, currentSquarePos[1] - diagonal });
                legalMoves.Add(new int[] { currentSquarePos[0] + diagonal, currentSquarePos[1] - diagonal });
                legalMoves.Add(new int[] { currentSquarePos[0] - diagonal, currentSquarePos[1] + diagonal });
            }

            print("mooooooooooooooo I want fooooooooooooooood and legooooooooo merch and a pet pigggggggyyyyyy");
        }

        if (chessPiece == chessPieceType.queen)
        {
            // print(currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
            for (int offset = 0; offset < 8; offset++)
            {
                legalMoves.Add(new int[] { currentSquarePos[0] + offset, currentSquarePos[1] + offset });
                legalMoves.Add(new int[] { currentSquarePos[0] - offset, currentSquarePos[1] - offset });
                legalMoves.Add(new int[] { currentSquarePos[0] + offset, currentSquarePos[1] - offset });
                legalMoves.Add(new int[] { currentSquarePos[0] - offset, currentSquarePos[1] + offset });

                legalMoves.Add(new int[] { currentSquarePos[0] + offset, currentSquarePos[1]});
                legalMoves.Add(new int[] { currentSquarePos[0] - offset, currentSquarePos[1]});
                legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + offset });
                legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] - offset });
            }
        }

        if (chessPiece == chessPieceType.king)
        {
            // print(currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
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
            // print(currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
            for (int offset = 0; offset < 8; offset++)
            {

                legalMoves.Add(new int[] { currentSquarePos[0] + offset, currentSquarePos[1] });
                legalMoves.Add(new int[] { currentSquarePos[0] - offset, currentSquarePos[1] });
                legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + offset });
                legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] - offset });
            }
        }

        if (chessPiece == chessPieceType.knight)
        {
            // print(currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
            legalMoves.Add(new int[] { currentSquarePos[0]+ 1, currentSquarePos[1] + 2});
            legalMoves.Add(new int[] { currentSquarePos[0]- 1, currentSquarePos[1] + 2 });
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
        GameObject.FindObjectOfType<Board_Manager>().selectedChessPiece = this;
        SetLegalMoves();
    }
}