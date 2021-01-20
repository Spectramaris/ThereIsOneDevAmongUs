using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    public new string name;
    public string sentence;
    public string effect;

    public Sprite artwork;

    public int manaCost;
    public EveryTypes.Rarity rarity;
}