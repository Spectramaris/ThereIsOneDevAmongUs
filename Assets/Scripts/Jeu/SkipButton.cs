using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public GameManager manager;

    private void OnMouseDown()
    {
        if(manager.playerTurn)
            manager.SkipTurn();
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = manager.playerTurn ? Color.green : Color.red;
    }
}
