using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    public GameManager manager { get; set; }
    public bool playerOwner { get; set; }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(playerOwner);
        transform.GetChild(1).gameObject.SetActive(!playerOwner);
    }

    private void OnMouseDown()
    {
        if (manager.playerTurn) manager.CreateBoardCard(gameObject, transform.GetChild(0).GetComponent<CardPrefab>().minionSO, true);
    }

    private void OnMouseEnter()
    {
        if (!playerOwner) return;

        transform.localScale = Vector2.one * 2;
        transform.localPosition = new Vector2(transform.localPosition.x, 11);
    }

    private void OnMouseExit()
    {
        if (!playerOwner) return;

        transform.localScale = Vector2.one;
        transform.localPosition = new Vector2(transform.localPosition.x, 0);
    }
}
