using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardCardPrefab : MonoBehaviour
{
    [HideInInspector] public Minion minionSO;
    public TextMeshPro manaCost;
    public TextMeshPro AtkText;
    public TextMeshPro DefText;
    public TextMeshPro TypeText;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = minionSO.CarteSurLePlateau;

        manaCost.text = minionSO.manaCost.ToString();
        AtkText.text  = minionSO.attack.ToString();
        DefText.text  = minionSO.defense.ToString();
        TypeText.text = minionSO.type.ToString();
    }
}
