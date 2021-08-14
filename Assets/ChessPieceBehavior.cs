using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceBehavior : MonoBehaviour
{
    public int startPosX;
    public int startPosY;
    public enum chessPieceType { pawn, rook, knight, bishop, queen, king };
    public chessPieceType chessPiece;

    List<int[]> legalMoves;

    GameObject currentChessSquare;

    // Start is called before the first frame update
    void Start()
    {
        legalMoves = new List<int[]>();
        SetStartPos();
        SetLegalMoves();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePiece(Vector3 squarepos)
    {
        foreach (int[] movePos in legalMoves)
        {
            if (squarepos.x == movePos[0] && squarepos.z == movePos[1])
            {
                transform.position = new Vector3(squarepos.x, 1f, squarepos.z);
                currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[] { (int)squarepos.x, (int)squarepos.z });
                SetLegalMoves();
                return;
            }
        }
        print("no legal move");
    }

    void SetStartPos()
    {
        currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[] { startPosX, startPosY });
        transform.position = new Vector3(currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[0], 1f, currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId[1]);
    }

    void SetLegalMoves()
    {
        if (chessPiece == chessPieceType.pawn)
        {
            legalMoves.Clear();
            // print(currentChessSquare.name);
            int[] currentSquarePos = currentChessSquare.GetComponent<Chess_Square_Info>().chessSquareId;
            legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 1 });
            legalMoves.Add(new int[] { currentSquarePos[0], currentSquarePos[1] + 2 });
        }
    }
}
