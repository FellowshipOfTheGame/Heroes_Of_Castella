using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class Battler : NetworkBehaviour
{
    private Animator anim;
    private MeshFilter meshFilter;
    public void Awake(){
        anim = GetComponent<Animator>();
        meshFilter = GetComponent<MeshFilter>();
    }
    // Dependendo do tipo de cerebro desejado adiciona um tipo de componente diferente
    // e salva a referencia pra ele nessa variavel
    private Brain brain;
    public PlayerHub Player { get{ return (brain == null)? null : brain.Player;} }
    [SyncVar]
    private int id;
    public int ID {
        get
        {
            return id;
        }
        set
        {
            if (isServer) id = value;
        }
    }
    public void YourTurn(){
        brain.RequestDecision();
    }

    [ClientRpc]
    public void RpcLoadMesh(string name){
        meshFilter.mesh = AssetManager.Meshes.LoadAsset<Mesh>(name);
    }

    [ClientRpc]
    public void RpcLoadAnimator(string name){
        // Provisorio, precisamos conversar com galera da arte
        anim.runtimeAnimatorController = AssetManager.Animators.LoadAsset<RuntimeAnimatorController>(name);
    }
}
