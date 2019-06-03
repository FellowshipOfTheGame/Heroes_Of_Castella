using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerHub : NetworkBehaviour
{
    // Deve ser um "singleton" para os clientes
    private static PlayerHub instance = null;
    public static PlayerHub Instance{ get{ return instance; } }
    public void Awake(){
        if(isLocalPlayer)
            instance = this;
    }

    [Command]
    public void CmdSendNewBattler(byte[] serialBattleRef){
        BattlerRef battlerInfo;
        IFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream(serialBattleRef);
        battlerInfo = (BattlerRef)formatter.Deserialize(stream);
        // Create new battler
    }

    // Liga a UI de selecionar ação do battler encontrado com o id de parametro
    [TargetRpc]
    public void TargetRequestPlayerDecision(NetworkConnection conn, int battlerID){
    }

    // Recebe a acao escolhida pelo player e repassa pro turnmanager
    // Deve ser chamada quando a decisao do player for determinada atraves da UI
    [Command]
    public void CmdSendPlayerDecision(Action action){
        TurnManager.Instance.onDecisionReached.Invoke(action);
    }
}
