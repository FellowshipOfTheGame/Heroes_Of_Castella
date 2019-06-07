using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella
{
    [System.Serializable]
    public class Character
    {
        [System.Serializable]
        public struct Attributes {
            public int strength;
            public int dexterity;
            public int spirit;
            public int vitality;
            public int initiative;
        }

        [System.Serializable]
        public struct Personality
        {
            [Range(-1f, 1f)]
            public float empathy;
            [Range(-1f, 1f)]
            public float agressivity;
        }

        public string name = "NEMO";
        public Attributes attributes;
        public Personality personality;
        public Skill[] skills;
    }
}

