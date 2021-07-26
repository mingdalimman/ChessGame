using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceBehavior : MonoBehaviour
{
    public int startPosX;
    public int startPosY;

    GameObject currentChessSquare;

    // Start is called before the first frame update
    void Start()
    {
        SetStartPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePiece(Vector3 squarepos)
    {
        transform.position = new Vector3(squarepos.x, 1f, squarepos.z);
    }

    void SetStartPos()
    {
        print(GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[]{startPosX, startPosY}).name);
        //currentChessSquare = GameObject.FindObjectOfType<Board_Manager>().GetChessSquare(new int[]{startPosX, startPosY});
    }
}
