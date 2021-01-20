using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion", menuName = "Create card/Minion")]
public class Minion : Card
{
    public int attack;
    public int defense;

    public EveryTypes.CardType type;
    [Header("Sprites")]
    public Sprite CarteEnMain;
    public Sprite CarteSurLePlateau;
}