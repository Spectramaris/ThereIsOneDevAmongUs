using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    private List<Minion> minions;
    private List<Spell>  spells;

    void Awake()
    {
        minions = Resources.LoadAll<Minion>("Cards").ToList();
        spells = Resources.LoadAll<Spell>("Cards").ToList();
    }

    public int GetCardsCount()
    {
        return minions.Count + spells.Count;
    }

    public int GetMinionsCount()
    {
        return minions.Count;
    }

    public int GetSpellsCount()
    {
        return spells.Count;
    }

    public List<Card> GetAllCards()
    {
        List<Card> cards = new List<Card>();

        foreach (var minion in minions)
            cards.Add(minion);
        foreach (var spell in spells)
            cards.Add(spell);

        return cards;
    }

    public Card GetCardAtIndex(int index)
    {
        if (index < minions.Count)
            return minions[index];
        else if (index < minions.Count + spells.Count)
            return spells[index - minions.Count];
        else
            return null;
    }

    public List<Minion> GetAllMinions()
    {
        return minions;
    }

    public Minion GetMinionAtIndex(int index)
    {
        if (index < minions.Count)
            return minions[index];
        else
            return null;
    }

    public List<Spell> GetAllSpells()
    {
        return spells;
    }

    public Spell GetSpellAtIndex(int index)
    {
        if (index < spells.Count)
            return spells[index];
        else
            return null;
    }
}
