using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class ActionEvent : UnityEvent<Action>{}

public abstract class TurnManager : NetworkBehaviour
{
    public static TurnManager Instance { get; private set; }
    public ActionEvent onDecisionReached = new ActionEvent();
    private TurnQueue queue;
    public void Awake(){
        if(!Instance)
            Instance = this;
    }
    public void Start(){
        onDecisionReached.AddListener(DecisionReached);
    }

    // Recebe a acao que o battler atual escolheu, verifica e pede pra realizar
    [Server]
    private void DecisionReached(Action action){
    }

    [Server]
    private void InstantiateBattlers()
    {

    }
}
