using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [System.Serializable]
    public struct Attributes
    {
        public int strength;
        public int initiative;
    }

    public Attributes attributes;
    public IBrain brain;

    public int Health => attributes.strength * 10;

}
