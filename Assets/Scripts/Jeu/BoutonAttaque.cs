using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonAttaque : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SelectAttackingCard(transform.parent.parent.gameObject);
        transform.parent.parent.GetComponent<BoardMinion>().StartAttack();
        Destroy(transform.parent.gameObject);
    }
}