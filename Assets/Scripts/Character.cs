using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    // Struct com informações para networking
    [System.Serializable]
    public abstract class CharacterInfo{
        public string name;
        public int hp;
        public int mp;
    }
}
