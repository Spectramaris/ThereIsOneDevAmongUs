using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class DecksManager : MonoBehaviour
{
    public GameObject cardContent;
    public GameObject UICardPrefab;
    public GameObject deckContent;
    public GameObject deckPrefab;
    public TextMeshProUGUI maxCardstxt;

    private List<Card> cards;
    private Dictionary<Card, int>[] decks = new Dictionary<Card, int>[5];
    private Dictionary<Card, int> tempDeck;
    private int actualDeckSelected;
    private int actualCardsCount;
    private int deckSelectedForGame;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Chargement de la liste des cartes dans la fenêtre de création des decks
        cards = GetComponent<CardsManager>().GetAllCards();

        // Placement de toutes les cartes du jeu dans la zone de sélection de la fenêtre de création des decks
        foreach (var card in cards)
        {
            GameObject newCard = Instantiate(UICardPrefab, cardContent.transform);
            newCard.name = card.name;
            newCard.GetComponent<CardPrefabUI>().cardSO = (Minion)card;
        }

        // Chargement des decks déjà possédés par le joueur
        for (int i = 0; i < 5; i++)
        {
            decks[i] = new Dictionary<Card, int>();

            var deck = PlayerPrefs.GetString($"Deck{i}");
            Debug.Log($"Deck {i + 1} : {deck}");
            for (int n = 0; n < deck.Length / 4; n++)
            {
                string cardSS = deck.Substring(n * 4, 4);
                decks[i].Add(cards[int.Parse(cardSS.Substring(1))], int.Parse($"{cardSS[0]}"));
            }
        }
    }

    public void AddCardToDeck(Card card)
    {
        // Empêche de rajouter plus de 30 cartes
        if (actualCardsCount == 30)
            return;

        if (tempDeck.ContainsKey(card)) // Si est déjà présente dans le deck
        {
            tempDeck[card]++;

            deckContent.transform.Find(card.name).GetChild(0).GetComponent<TextMeshProUGUI>().text = tempDeck[card].ToString();
        }
        else // Si n'est pas déjà placée dans le deck
        {
            tempDeck.Add(card, 1);

            GameObject newCard = Instantiate(deckPrefab, deckContent.transform);
            newCard.name = card.name;
            newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1";
            newCard.GetComponent<CardPrefabUIResume>().minionSO = (Minion)card;
        }

        checkCardLock(card);
        UpdateCardsCount();
    }

    public void RemoveCardToDeck(Card card)
    {
        if (tempDeck[card] == 1) // S'il ne reste qu'un exemplaire de la carte
        {
            tempDeck.Remove(card);

            Destroy(deckContent.transform.Find(card.name).gameObject);
        }
        else // Si la carte existe en plus d'un exemplaire
        {
            tempDeck[card]--;

            deckContent.transform.Find(card.name).GetChild(0).GetComponent<TextMeshProUGUI>().text = tempDeck[card].ToString();
        }

        checkCardLock(card);
        UpdateCardsCount();
    }

    public void CancelModifications()
    {
        tempDeck = null;
    }

    public void ValidateDeckModifications()
    {
        decks[actualDeckSelected] = tempDeck;

        // Encryptage du deck en int (Un chiffre pour le nombre d'exemplaires, trois pour l'ID de la carte)
        string deckInString = "";
        foreach (var pair in tempDeck)
        {
            deckInString += $"{pair.Value}{(cards.IndexOf(pair.Key)):D3}";
        }

        PlayerPrefs.SetString($"Deck{actualDeckSelected}", deckInString);
        PlayerPrefs.Save();
    }

    public void SelectionDeck(int deckSelectionned)
    {
        Debug.Log(decks[deckSelectionned].Count);
        actualDeckSelected = deckSelectionned;
        tempDeck = decks[actualDeckSelected].ToDictionary(entry => entry.Key, entry => entry.Value);

        // Clean de l'affichage du deck
        foreach (Transform child in deckContent.transform)
        {
            Destroy(child.gameObject);

            checkCardLock(child.GetComponent<CardPrefabUIResume>().minionSO);
        }

        // Remplissage de l'affichage du deck
        foreach (var pair in tempDeck)
        {
            GameObject newCard = Instantiate(deckPrefab, deckContent.transform);
            newCard.name = pair.Key.name;
            newCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pair.Value.ToString();
            newCard.GetComponent<CardPrefabUIResume>().minionSO = (Minion) pair.Key;

            checkCardLock(pair.Key);
        }

        UpdateCardsCount();
    }

    public Dictionary<Card, int> GetDeck(int deckNum)
    {
        if (deckNum >= 0 && deckNum < 5)
            return decks[deckNum];
        else
            return null;
    }

    private void UpdateCardsCount()
    {
        actualCardsCount = 0;

        foreach (var pair in tempDeck)
            actualCardsCount += pair.Value;

        maxCardstxt.text = $"Cartes : {actualCardsCount}/30";
    }

    private void checkCardLock(Card card)
    {
        GameObject cardGO = GameObject.Find(card.name);
        bool maxReached = false;
        if (tempDeck.ContainsKey(card))
        {
            switch (card.rarity)
            {
                case EveryTypes.Rarity.Commune:
                    if (tempDeck[card] == 3)
                        maxReached = true;
                    break;

                case EveryTypes.Rarity.Rare:
                    if (tempDeck[card] == 3)
                        maxReached = true;
                    break;

                case EveryTypes.Rarity.Epique:
                    if (tempDeck[card] == 2)
                        maxReached = true;
                    break;

                case EveryTypes.Rarity.Legendaire:
                    maxReached = true;
                    break;
            }
        }

        cardGO.GetComponent<CardUIDragNDrop>().enabled = !maxReached;
        cardGO.GetComponent<Image>().color = maxReached ? Color.red : Color.white;
    }

    public void SelectPlayDeck(int i) => deckSelectedForGame = i;
    public Dictionary<Card, int> GetPlayDeck() => decks[deckSelectedForGame];
}