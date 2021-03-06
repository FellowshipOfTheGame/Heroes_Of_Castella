﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace HeroesOfCastella
{
    [System.Serializable]
    [CreateAssetMenu]
    public class Skill : ScriptableObject, ISkill
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

        //public string name;
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
            if (agent.Map.GetElementAt(target[0]) is Battler targetBattler) // targetBattler != null, it should be
            {
                targetBattler.HP -= (agent as Battler).character.attributes.strength;
                (agent as Battler).HP -= 1;
                Debug.Log(" ===== ACTION TIME! ===== " + agent.GetName() + " uses skill against " + targetBattler.GetName());
                return true;
            }
            return false;            
        }

        public byte[] Serialized()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, this.name);
            return stream.GetBuffer();
        }

        public void Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string skillName = (string)binaryFormatter.Deserialize(stream);

        }
    }

    
}

