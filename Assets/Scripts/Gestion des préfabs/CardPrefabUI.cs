using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardPrefabUI : MonoBehaviour
{
    public Card cardSO;

    public TextMeshProUGUI nameText;

    void Start()
    {
        nameText.text = cardSO.name;
    }
}
