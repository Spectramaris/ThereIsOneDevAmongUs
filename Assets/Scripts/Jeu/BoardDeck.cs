using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDeck : MonoBehaviour
{
    public bool isPlayerOwner;
    public GameManager manager;

    private void OnMouseDown()
    {
        manager.SelectEnnemyPlayer(isPlayerOwner);
    }
}
