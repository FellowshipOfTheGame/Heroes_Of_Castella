using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRules
{
    public static void InitializeInitiative(List<Battler> battlers)
    {
        foreach (Battler b in battlers)
        {
            b.actionPoints = b.character.attributes.initiative;
        }
    }
    //Game Rules referencing Battler? Doesn't seem right
    public static void StepTime(List<Battler> battlers)
    {
        foreach (Battler b in battlers)
        {
            b.actionPoints += b.character.attributes.initiative;
        }
        //battleField.weather.update() - suggestion
    }
    

    public static float SkillToHit(Action action)
    {
        return action.skill.baseToHit;
    }

    public static float Damage (Action action) {
        return Mathf.RoundToInt(action.agent.character.attributes.strength * action.skill.damageMult - action.target.character.attributes.strength);
    }
}
