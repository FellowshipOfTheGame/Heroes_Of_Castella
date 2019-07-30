using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrainType{
    Brain_Player,
    Brain_Random,
    Brain_Basic
}

[System.Serializable]
public struct BrainSerializable{
    public BrainType type;
    public byte[] data;
}

public interface IBrain
{
    BrainSerializable Serialized();
    void Deserialize(byte[] data);
    void Initialize(ITurnTaker turnTaker, IBattleMap battleMap); // Who am I? What is the world I'm in?
    void ChooseAction();
    event OnActionChosenDelegate OnActionChosen;
}