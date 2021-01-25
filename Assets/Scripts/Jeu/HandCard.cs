using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    public GameManager manager { get; set; }
    public bool playerOwner { get; set; }

    [HideInInspector] public bool overrideAnimation = false;
    [HideInInspector] public Vector2 positionToOverride;
    [HideInInspector] public Vector2 sizeToOverride;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(playerOwner);
        transform.GetChild(1).gameObject.SetActive(!playerOwner);

        sizeToOverride = Vector2.one;
        Debug.Log(sizeToOverride);
    }

    private void LateUpdate()
    {
        if (!overrideAnimation)
            return;

        transform.position = positionToOverride;
        transform.localScale = sizeToOverride;
    }

    private void OnMouseDown()
    {
        if (manager.playerTurn) manager.CreateBoardCard(gameObject, transform.GetChild(0).GetComponent<CardPrefab>().minionSO, GetComponent<Animator>(),true);
    }

    private void OnMouseEnter()
    {
        if (!playerOwner) return;

        positionToOverride.y = -3;
        sizeToOverride = Vector2.one * 2;
    }

    private void OnMouseExit()
    {
        if (!playerOwner) return;

        sizeToOverride = Vector2.one;
        positionToOverride.y = -14;
    }

    public void DrawEnd()
    {
        overrideAnimation = true;
        manager.isAnimationPlaying = false;
    }
}
