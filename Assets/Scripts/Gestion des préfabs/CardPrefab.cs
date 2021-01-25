using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardPrefab : MonoBehaviour
{
    public Minion minionSO;

    public TextMeshPro manaCostText;
    public TextMeshPro atkText;
    public TextMeshPro defText;
    public TextMeshPro nameText;
    public TextMeshPro attacknameText;
    public TextMeshPro typeText;
    public TextMeshPro effectText;

    public bool isPlayer;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = minionSO.CarteEnMain;
        transform.parent.GetComponent<Animator>().SetBool("IsPlayer", isPlayer);

        manaCostText.text   = minionSO.manaCost.ToString();
        atkText.text        = minionSO.attack.ToString();
        defText.text        = minionSO.defense.ToString();
        nameText.text       = minionSO.name;
        attacknameText.text = minionSO.sentence;
        typeText.text       = minionSO.type.ToString();
        effectText.text     = minionSO.effect; 
    }
}
