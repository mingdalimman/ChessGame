using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess_Square_Behavior : MonoBehaviour
{
    private void OnMouseDown()
    {


        GameObject.FindObjectOfType<ChessPieceBehavior>().MovePiece(transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
