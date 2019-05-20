using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    public int cost;
    public int range;
    public float damageMult;
    public float baseToHit;


    public static float ToHitChance(Action action)
    {
        return GameRules.SkillToHit(action);
    }

    public static float Damage(Action action)
    {
        return GameRules.Damage(action);
    }
}
