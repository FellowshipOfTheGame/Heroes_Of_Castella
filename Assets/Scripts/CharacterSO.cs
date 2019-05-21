using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    public Character character;

    public void Awake()
    {
        //character.IterationOnStruct();
    }

    public void Refresh()
    {

    }
}
