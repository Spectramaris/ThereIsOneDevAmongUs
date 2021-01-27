using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationDeck : MonoBehaviour
{
    [SerializeField] private DecksManager decksManager;

    [Header("Cartes")]
    [SerializeField] private Sprite carte;
    [SerializeField] private Sprite carteOr;

    [Header("Decks")]
    [SerializeField] private Sprite[] decks = new Sprite[5];

    [Header("Decks dorés")]
    [SerializeField] private Sprite[] decksOr = new Sprite[5];

    private List<GameObject> decksImages = new List<GameObject>();

    void Start()
    {
        foreach(Transform child in transform)
        {
            Debug.Log("child : " + child.name);
            decksImages.Add(child.gameObject);
        }    
    }

    void Update()
    {
        for(int i = 0;i < decksImages.Count; i++)
        {
            if (decksManager.GetCountForDeck(i) == 30)
            {
                decksImages[i].GetComponent<Image>().sprite = carteOr;
                decksImages[i].transform.GetChild(0).GetComponent<Image>().sprite = decksOr[i];
            }
            else
            {
                decksImages[i].GetComponent<Image>().sprite = carte;
                decksImages[i].transform.GetChild(0).GetComponent<Image>().sprite = decks[i];
            }
        }
    }
}
