using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella
{
    [System.Serializable]
    public class Skill : ISkill
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

        //ISkill
        public bool Apply(IBattler agent, params Vector3[] target)
        {
            if (agent.GetType() == typeof(IBattler))
            {
                
            }
            throw new System.NotImplementedException();
        }
        
    }

    
}

