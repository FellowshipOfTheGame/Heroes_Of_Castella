using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public struct Action{
    public int agentID;
    public int skillIndex;
    public Vector2 target;
}

// Lista de todos os tipos de IA implementadas
public enum AIType{
    basic = 0
}


public abstract class Brain : NetworkBehaviour
{
    private Battler battler;
    private PlayerHub player;
    public PlayerHub Player { get { return player; } }
    private bool isPlayerControlled;
    private float[] AIParameters;

    public void Start(){
        battler = GetComponent<Battler>();
    }

    public void RequestDecision(){
        if(isPlayerControlled){
            player.TargetRequestPlayerDecision(player.connectionToClient, battler.ID);
        }else{
            AIdecision();
        }
    }
    
    // Função da IA que escolhe uma decisão, precisa chamar o evento do turnManager
    // igual a chamada da funcao CmdSendPlayerDecision do Playerhub
    protected abstract void AIdecision();
}
