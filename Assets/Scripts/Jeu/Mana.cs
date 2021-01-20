using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int manaPool { get; set; }
    public int actualMana { get; set; }

    private SpriteRenderer[] srTab = new SpriteRenderer[10];

    void Start()
    {
        for (int i = 0; i < 10; i++)
            srTab[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();

        manaPool = 1;
        ResetActualMana();
    }

    public void AddManaInPool(int number)      => manaPool = Mathf.Clamp(manaPool + number, 0, 10);
    public void RemoveManaFromPool(int number) => manaPool = Mathf.Clamp(manaPool - number, 0, 10);

    public void AddMana(int number)
    {
        actualMana = Mathf.Clamp(actualMana + number, 0, manaPool);
        RefreshSR();
    }

    public void RemoveMana(int number)
    {
        actualMana = Mathf.Clamp(actualMana - number, 0, manaPool);
        RefreshSR();
    }

    public void ResetActualMana()
    {
        actualMana = manaPool;
        RefreshSR();
    }

    private void RefreshSR()
    {
        for (int i = 0; i < actualMana; i++)
            srTab[i].enabled = true;
        for (int i = actualMana; i < 10; i++)
            srTab[i].enabled = false;
    }
}
