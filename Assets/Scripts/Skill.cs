using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public enum Type
    {
        MELEE,
        PROJECTILE,
        DIRECT
    }

    public enum TargetType
    {
        ENEMIES,
        ALLIES
    }

    public enum BaseDamage
    {
        STRENGTH,
        INTELLIGENCE,
        FREE_ENCUMBRANCE
    }

    [Header("Type")]
    public Type type;
    public TargetType targetType;
    public BaseDamage baseDamage;
    public bool damageToActionPoints = false;
    public bool lifeDrain = false;
    [Header("Values")]
    public int hitsCount = 1;
    public int range = 1;
    public int extraDamage = 0;
    public int extraCost = 0;
    [Header("Effect Area")]
    [HideInInspector]
    public bool[] targetShape = new bool[15];
}
