using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMinion : MonoBehaviour
{
    public Minion minionSO { get; set; }

    public int manaCost { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public EveryTypes.CardType type { get; set; }
    public bool playerOwner { get; set; }
    private EveryTypes.BoardMode _actualMode;
    private bool buttonsDestroyed = false;

    public EveryTypes.BoardMode ActualMode
    {
        get { return _actualMode; }
        set {
            if (canChangeMode)
            {
                _actualMode = value;
                canChangeMode = false;

                transform.Rotate(0, 0, value == EveryTypes.BoardMode.Attaque ? 90 : -90);
            }
        }
    }

    public GameObject boutonsPrefab;
    private GameObject boutons;
    private bool canChangeMode = true;
    private bool canAttack = false;
    private bool attacking = false;

    private void Start()
    {
        manaCost = minionSO.manaCost;
        atk      = minionSO.attack;
        def      = minionSO.defense;
        type     = minionSO.type;
    }

    private void Update()
    {
        if (boutons == null) return;

        if (Input.GetMouseButtonDown(0) && buttonsDestroyed == true)
            Destroy(boutons.gameObject);

        if(attacking && Input.GetMouseButtonDown(1))
        {
            canAttack = true;
            attacking = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().AvortAttackingCard();
        }
    }

    public void StartAttack()
    {
        canAttack = false;
        canChangeMode = false;
        attacking = true;
    }

    public void HasAttacked() => attacking = false;

    private void OnMouseUp()
    {
        buttonsDestroyed = true;
    }

    private void OnMouseDown()
    {
        if (!playerOwner)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SelectEnnemyCard(gameObject);
            return;
        }

        if (boutons != null)
            return;
        if (!canAttack && !canChangeMode)
            return;

        buttonsDestroyed = false;
        boutons = Instantiate(boutonsPrefab, transform);
        boutons.transform.localPosition = Vector3.zero;
        if (ActualMode == EveryTypes.BoardMode.Defense)
            boutons.transform.Rotate(0, 0, 90);

        if (!canAttack || ActualMode == EveryTypes.BoardMode.Defense)
            boutons.transform.GetChild(2).gameObject.SetActive(false);

        if (!canChangeMode)
        {
            boutons.transform.GetChild(0).gameObject.SetActive(false);
            boutons.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            boutons.transform.GetChild(ActualMode == EveryTypes.BoardMode.Attaque ? 0 : 1).gameObject.SetActive(false);
    }

    public void NextTurn()
    {
        canChangeMode = true;
        canAttack = true;
    }
}
