using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardPrefabUI : MonoBehaviour
{
    public Minion cardSO;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI attaqueText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI manacostText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI AttaqueNameText;
    public TextMeshProUGUI AttaqueText;

    void Start()
    {
        GetComponent<Image>().sprite = cardSO.CarteEnMain;

        nameText.text = cardSO.name;
        attaqueText.text = cardSO.attack.ToString();
        defText.text = cardSO.defense.ToString();
        manacostText.text = cardSO.manaCost.ToString();
        typeText.text = cardSO.type.ToString();
        AttaqueNameText.text = cardSO.sentence;
        attaqueText.text = cardSO.effect;
    }
}
