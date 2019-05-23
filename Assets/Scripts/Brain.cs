using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Action{
    Battler agent;
    int skillIndex;
    Vector2 target;
}

public class ActionEvent : UnityEvent<Action>{}

public abstract class Brain
{
    public ActionEvent onDecisionReached = new ActionEvent();
    private PlayerHub player;
    private bool isPlayerControlled;
    private float[] AIParameters;

    public void Start(){
        onDecisionReached.AddListener(DecisionReached);
    }

    public void RequestDecision(){
        // to do
    }
    
    protected abstract void AIdecision();

    private void DecisionReached(Action selectedAction){

    }
}
