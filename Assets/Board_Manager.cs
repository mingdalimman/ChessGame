using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Manager : MonoBehaviour
{
    public GameObject[,] chessSquares;
    public GameObject chessSquarePrefab;
    public GameObject chessSquareDarkPrefab;

    public ChessPieceBehavior selectedChessPiece;

    public ChessPieceBehavior.chessPieceColor currentTurnColor;

    // Start is called before the first frame update
    void Awake()
    {
        chessSquares = new GameObject[8,8];

        for(int i=0; i < 8; i++)
        {
            for(int j = 0; j < 8; j ++)
            {
                int oddOrEven = 0;

                if(j%2 == 0)
                {
                    oddOrEven = 2;
                }
                else
                {
                    oddOrEven = 1;
                }

                if((i + oddOrEven) % 2 == 0)
                {
                    chessSquares[i,j] = Instantiate(chessSquarePrefab, new Vector3(i,0,j), Quaternion.identity);
                }
                else
                {
                    chessSquares[i,j] = Instantiate(chessSquareDarkPrefab, new Vector3(i,0,j), Quaternion.identity);
                }
                chessSquares[i,j].GetComponent<Chess_Square_Info>().chessSquareId = new int[2] {i,j};
                
            }

        }

    }

    public void SwitchTurn()
    {
        if(currentTurnColor == ChessPieceBehavior.chessPieceColor.white)
        {
            currentTurnColor = ChessPieceBehavior.chessPieceColor.black;
        }
        else
        {
            currentTurnColor = ChessPieceBehavior.chessPieceColor.white;
        }
    }

    public GameObject GetChessSquare(int[] startPos)
    {
        return chessSquares[startPos[0], startPos[1]];
    }
    
}
