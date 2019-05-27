using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Battler : NetworkBehaviour
{
    // Dependendo do tipo de cerebro desejado adiciona um tipo de componente diferente
    // e salva a referencia pra ele nessa variavel
    private Brain brain;
    public PlayerHub Player { get{ return (brain == null)? null : brain.Player;} }
    [SyncVar]
    private int id;
    public int ID { get { return id; } }
    public void YourTurn(){
        brain.RequestDecision();
    }
}
