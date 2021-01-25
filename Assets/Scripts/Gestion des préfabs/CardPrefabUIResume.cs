using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardPrefabUIResume : MonoBehaviour
{
    public Minion minionSO;

    public TextMeshProUGUI nameText;
    public Image image;
    private void Start()
    {
        nameText.text = minionSO.name;
    }
}
