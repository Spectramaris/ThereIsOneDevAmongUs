using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Create card/Spell")]
public class Spell : Card
{
    public EveryTypes.SpellType type;
}