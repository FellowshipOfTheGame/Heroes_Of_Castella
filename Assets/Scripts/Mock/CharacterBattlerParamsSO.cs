using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroesOfCastella;

namespace Mock
{
    [CreateAssetMenu]
    public class CharacterBattlerParamsSO : ScriptableObject
    {
        public CharacterSO characterSO;
        public Vector3 position;
        public BrainSO brainSO;
    }
}

