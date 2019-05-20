using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler
{
    public CharacterSO character;
    public int actionPoints; // Not generic here
    public IBrain brain;
    public int health;


    public delegate void OnBattlerTurnDelegate(Battler battler);

    public Battler(CharacterSO character)
    {
        this.character = character;
        this.brain = character.brain;
        this.health = character.Health;
    }
}
