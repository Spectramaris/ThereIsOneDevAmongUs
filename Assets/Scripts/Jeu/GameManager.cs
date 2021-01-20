using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject handCardPrefab;
    public GameObject boardCardPrefab;

    [Header("Joueurs")]
    public Transform playerTransform;
    public Transform playerBoardTransform;
    public Mana playerMana;
    public Transform aiTransform;
    public Transform aiBoardTransform;
    public Mana aiMana;  

    [Header("Affichage")]
    public TextMeshPro TextChronometre;
    public TextMeshPro hpJoueurText;
    public TextMeshPro hpAiText;

    // Joueur 
    private int hp = 10000;
    private int tourSansCarte = 0;
    private List<Card> deck  = new List<Card>();
    private List<GameObject> hand  = new List<GameObject>();
    private List<GameObject> board = new List<GameObject>();

    // IA
    private int aiHp = 10000;
    private int aiTourSansCarte = 0;
    private List<Card> aiDeck  = new List<Card>();
    private List<GameObject> aiHand  = new List<GameObject>();
    private List<GameObject> aiBoard = new List<GameObject>();

    [HideInInspector] public bool playerTurn { get; set; } = true;
    private Coroutine  chronometre;
    private GameObject playerAttacking;

    
    //---RULES---//
    int maxCardsInHand = 8;
    int maxCardsOnBoard = 5;
    int startingCardsNumber = 4;
    float dureeDeTour = 60;

    int HandCardWidth = 6;
    int BoardCardWidth = 6;


    private void Start()
    {
        // Récupération du Deck
        var deckDic = GameObject.Find("Menu principal").GetComponent<DecksManager>().GetPlayDeck();
        foreach (var pair in deckDic)
            for (int i = 0; i < pair.Value; i++)
                deck.Add(pair.Key);

        // Pour le bien du jeu, l'IA commence avec le même deck que le joueur
        aiDeck = new List<Card>(deck);

        // Création des cartes de départ
        for (int i = 0; i < startingCardsNumber; i++)
        {
            int cardRandom = Random.Range(0, deck.Count);
            int aiCardRandom = Random.Range(0, deck.Count);

            CreateHandCard(deck[cardRandom], true);
            CreateHandCard(aiDeck[aiCardRandom], false);
        }

        chronometre = StartCoroutine(Chronometre());
    }

    //---Gestion des joueurs---//
    private void SubirDegats(int value, bool isPlayer)
    {
        if (isPlayer)
        {
            aiHp += value;
            hpAiText.text = $"{aiHp}";
        }
        else
        {
            hp += value;
            hpJoueurText.text = $"{hp}";
        }
    }

    //---Gestion du terrain---//
    public bool CreateBoardCard(GameObject cardGO, Minion cardSO, bool isPlayer)
    {
        var tempBoard          = isPlayer ? board                : aiBoard;
        var tempBoardTransform = isPlayer ? playerBoardTransform : aiBoardTransform;
        var tempMana           = isPlayer ? playerMana           : aiMana;
        var tempHand           = isPlayer ? hand                 : aiHand;

        if (tempBoard.Count == maxCardsOnBoard)
            return false;
        if (cardSO.manaCost > tempMana.actualMana)
            return false;

        tempMana.RemoveMana(cardSO.manaCost);

        var boardCard = Instantiate(boardCardPrefab);
        boardCard.GetComponent<BoardCardPrefab>().minionSO = cardSO;
        boardCard.GetComponent<BoardMinion>().minionSO = cardSO;
        boardCard.GetComponent<BoardMinion>().playerOwner = playerTurn;

        boardCard.transform.position = new Vector2(0, tempBoardTransform.position.y);
        tempHand.Remove(cardGO);
        tempBoard.Add(boardCard);

        Destroy(cardGO);
        RefreshHandPlacement(isPlayer);
        RefreshBoardPlacement(isPlayer);

        return true;
    }

    public void RefreshBoardPlacement(bool isPlayer)
    {
        var tempBoard = isPlayer ? board : aiBoard;
        int cardPosition = -(tempBoard.Count * (BoardCardWidth / 2)) + (BoardCardWidth / 2);

        foreach(var card in tempBoard)
        {
            card.transform.position = new Vector2(cardPosition, card.transform.position.y);
            cardPosition += BoardCardWidth;
        }
    }

    public void SelectAttackingCard(GameObject card) => playerAttacking = card;
    public void AvortAttackingCard() => playerAttacking = null;

    public bool SelectEnnemyPlayer(bool isPlayer)
    {
        if (playerAttacking == null) return false;

        var tempEnnemyBoard = isPlayer ? board : aiBoard;
        foreach(var boardMinion in tempEnnemyBoard)
        {
            if(boardMinion.GetComponent<BoardMinion>().ActualMode == EveryTypes.BoardMode.Defense)
            {
                Debug.Log("Un sbire est en défense, impossible d'attaquer le joueur ennemi !");
                return false;
            }
        }

        SubirDegats(-(playerAttacking.GetComponent<BoardMinion>().atk), !isPlayer);

        playerAttacking.GetComponent<BoardMinion>().HasAttacked();
        playerAttacking = null;

        return true;
    }

    public void SelectEnnemyCard(GameObject card)
    {
        if (playerAttacking == null) return;

        int attaquantStat = playerAttacking.GetComponent<BoardMinion>().atk;
        var ennemyBoard = playerTurn ? aiBoard : board;
        var allyBoard = playerAttacking ? board : aiBoard;
        BoardMinion defenseur = card.GetComponent<BoardMinion>();
        int defenseurStat = defenseur.ActualMode == EveryTypes.BoardMode.Attaque ? defenseur.atk : defenseur.def;

        if (attaquantStat > defenseurStat)
        {
            ennemyBoard.Remove(defenseur.gameObject);
            Destroy(defenseur.gameObject);
            if (defenseur.ActualMode == EveryTypes.BoardMode.Attaque)
                SubirDegats(defenseurStat - attaquantStat, true);
        }
        else if (attaquantStat < defenseurStat)
        {
            SubirDegats(attaquantStat - defenseurStat, false);
            if (defenseur.ActualMode == EveryTypes.BoardMode.Attaque)
            {
                allyBoard.Remove(playerAttacking);
                Destroy(playerAttacking);
            }
        }
        else
        {
            if(defenseur.ActualMode == EveryTypes.BoardMode.Attaque)
            {
                allyBoard.Remove(playerAttacking);
                ennemyBoard.Remove(defenseur.gameObject);
                Destroy(playerAttacking);
                Destroy(defenseur.gameObject);
            }
        }

        playerAttacking.GetComponent<BoardMinion>().HasAttacked();
        playerAttacking = null;
    }

    //---Gestion de la main---//
    public void DrawCard(bool isPlayer)
    {
        var tempDeck = isPlayer ? deck : aiDeck;

        if(tempDeck.Count == 0)
        {
            if(isPlayer)
            {
                tourSansCarte += 1;
                SubirDegats(tourSansCarte * -100, true);
            }
            else
            {
                aiTourSansCarte += 1;
                SubirDegats(tourSansCarte * -100, false);
            }

            return;
        }

        int cardRandom = Random.Range(0, tempDeck.Count);
        CreateHandCard(tempDeck[cardRandom], isPlayer);

        Debug.Log($"Il reste {tempDeck.Count} cartes dans le deck");
    }

    public void CreateHandCard(Card cardSO, bool isPlayer)
    {
        var tempDeck = isPlayer ? deck : aiDeck;
        var tempHand = isPlayer ? hand : aiHand;

        tempDeck.Remove(cardSO);
        if (tempHand.Count == maxCardsInHand)
            return;

        var card = Instantiate(handCardPrefab);
        card.transform.GetChild(0).GetComponent<CardPrefab>().minionSO = (Minion)cardSO;
        card.transform.SetParent(isPlayer ? playerTransform : aiTransform);
        card.transform.localPosition = Vector2.zero;
        card.GetComponent<HandCard>().manager = this;
        card.GetComponent<HandCard>().playerOwner = isPlayer;

        tempHand.Add(card);

        RefreshHandPlacement(isPlayer);
    }

    public void RefreshHandPlacement(bool isPlayer)
    {
        var tempHand = isPlayer ? hand : aiHand;
        int cardPosition = -(tempHand.Count * (HandCardWidth / 2)) + (HandCardWidth / 2);

        foreach (var card in tempHand)
        {
            card.transform.position = new Vector2(cardPosition, card.transform.position.y);
            cardPosition += HandCardWidth;
        }
    }

    //---Gestion du chronomètre---//
    private IEnumerator Chronometre()
    {
        float temps = 0;

        TextChronometre.text = "60";
        yield return new WaitForSeconds(1);

        while (temps < 60)
        {
            temps = (Time.deltaTime + temps);
            TextChronometre.text = $"{(int)(dureeDeTour - temps)}";

            yield return new WaitForEndOfFrame();
        }

        NextTurn();
    }

    public void SkipTurn()
    {
        if (chronometre == null) return;

        StopCoroutine(chronometre);
        NextTurn();
    }

    private void NextTurn()
    {
        playerTurn = !playerTurn;
        DrawCard(playerTurn);

        var tempMana = playerTurn ? playerMana : aiMana;
        var tempBoard = playerTurn ? board : aiBoard;
        tempMana.AddManaInPool(1);
        tempMana.ResetActualMana();

        foreach (var boardCard in tempBoard)
            boardCard.GetComponent<BoardMinion>().NextTurn();

        if(!playerTurn)
        {
            AIJouerCartes();
            AIActionsBoard();

            NextTurn();
        }

        chronometre = StartCoroutine(Chronometre());
    }

    //---Gestion de la logique de l'IA---//
    private void AIJouerCartes()
    {
        var tempHand = new List<GameObject>(aiHand);

        foreach (var card in tempHand)
            if(!CreateBoardCard(card, card.transform.GetChild(0).GetComponent<CardPrefab>().minionSO, false))
                break;
    }

    private void AIActionsBoard()
    {
        if (aiBoard.Count == 0)
            return;

        // Vérification du comparatif attaque / défense
        bool isOneDefenser = false;
        foreach(var card in aiBoard)
        {
            var cardScript = card.GetComponent<BoardMinion>();

            if (cardScript.def > cardScript.atk && cardScript.ActualMode == EveryTypes.BoardMode.Attaque)
            {
                cardScript.ActualMode = EveryTypes.BoardMode.Defense;
                isOneDefenser = true;
            }else if(cardScript.ActualMode == EveryTypes.BoardMode.Defense)
            {
                isOneDefenser = true;
            }
        }

        // Si aucune carte n'est en défense
        if (!isOneDefenser)
        {
            BoardMinion bestDefCard = aiBoard[0].GetComponent<BoardMinion>();
            foreach(var card in aiBoard)
            {
                var cardScript = card.GetComponent<BoardMinion>();

                if (cardScript.def > bestDefCard.def)
                    bestDefCard = cardScript;
            }

            bestDefCard.ActualMode = EveryTypes.BoardMode.Defense;
        }

        // Attaque
        List<GameObject> attaquants = aiBoard.Where(x => x.GetComponent<BoardMinion>().ActualMode == EveryTypes.BoardMode.Attaque).ToList();
        foreach (var card in attaquants)
        {
            var cardScript = card.GetComponent<BoardMinion>();
            bool hasAttacked = false;
            playerAttacking = card;

            Debug.Log("La carte " + cardScript.minionSO.name + " de l'ia tente d'attaquer");

            foreach (var playerCard in board)
            {
                var playerCardScript = playerCard.GetComponent<BoardMinion>();

                Debug.Log($"Elle se compare à la carte {playerCardScript.minionSO.name}");

                if((playerCardScript.ActualMode == EveryTypes.BoardMode.Attaque && cardScript.atk > playerCardScript.atk) ||
                    playerCardScript.ActualMode == EveryTypes.BoardMode.Defense && cardScript.atk > playerCardScript.def)
                {
                    Debug.Log($"Elle est supérieure, elle attaque");
                    SelectEnnemyCard(playerCard);
                    hasAttacked = true;
                    break;
                }
            }

            if (!hasAttacked)
            {
                Debug.Log("La carte n'a pas attaqué, elle attaque le joueur");
                SelectEnnemyPlayer(true);
            }
        }
    }
}
