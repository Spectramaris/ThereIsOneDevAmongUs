using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckPrefab : MonoBehaviour
{

    public Minion minionSO;

    public TextMeshPro manaCostText;

    public TextMeshPro nameText;
    // Start is called before the first frame update
    void Start()
    {
        manaCostText.text = minionSO.manaCost.ToString();

        nameText.text = minionSO.name;

    
    }

   
}
