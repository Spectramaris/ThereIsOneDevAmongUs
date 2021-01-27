using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EveryTypes
{
    public static bool GameIsLaunched { get; set; }

    public enum Rarity
    {
        Commune, Rare, Epique, Legendaire
    }

    public enum CardType
    {
        Graph, Dev, Scenar, GD, Tous, Prof, MAPIC, Sound
    }

    public enum SpellType
    {
        Majie, Piege, Terrain
    }

    public enum BoardMode
    {
        Attaque, Defense
    }
}
