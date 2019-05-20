using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class could be a part (component) of GameRules
public class TurnQueue
{
    List<Battler> battlers;

    public event Battler.OnBattlerTurnDelegate OnBattlerTurn;


    public TurnQueue(List<Battler> battlers)
    {
        this.battlers = battlers;
    }

    void Initialize()
    {
        GameRules.InitializeInitiative(battlers);
        foreach (Battler b in battlers)
        {
            OnBattlerTurn += b.brain.OnBattlerTurn;
        }
    }

    // Update is called once per frame
    public bool Update()
    {
        List<Battler> readyBattlers = new List<Battler>();
        foreach (Battler b in battlers)
        {
            if (b.actionPoints >= 12)
            {
                readyBattlers.Add(b);
            }
        }
        if (readyBattlers.Count == 0)
        {
            GameRules.StepTime(battlers);
            return true;
        }
        OnBattlerTurn?.Invoke(readyBattlers[0]);
        return false;
    }

    void Sort()
    {

    }
}
