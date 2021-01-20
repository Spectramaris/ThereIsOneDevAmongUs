using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellCardPrefab : MonoBehaviour
{
    public Spell spellSO;

    public TextMeshPro manaText;

    public TextMeshPro nameText;

    public TextMeshPro sentenceText;

    public TextMeshPro effectText;


    // Start is called before the first frame update
    void Start()
   
    {
        manaText.text = spellSO.manaCost.ToString();

        nameText.text = spellSO.name.ToString();

        sentenceText.text = spellSO.sentence;

        effectText.text = spellSO.effect;

        



    }
}
