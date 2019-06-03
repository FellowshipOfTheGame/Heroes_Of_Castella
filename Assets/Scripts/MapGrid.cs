using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Grid))]
public abstract class MapGrid : NetworkBehaviour
{
    public static MapGrid Instance {get; protected set;}
    public GameObject battlerPrefab;
    private Grid grid;
    public void Awake(){
        if(!Instance){
            // Pode ser aprimorado
            Instance = this;
        }

        grid = GetComponent<Grid>();
    }

    protected virtual Vector2 TransformCoordinates(Vector2 localCoordinates, int team){
        // Altera a posição do battler do sistema de coordenadas local da seleção
        // para o sistema global da batalha
        if(team == 1){
            localCoordinates.x *= -1;
            localCoordinates.x -= 1;
            localCoordinates.y *= -1;
        }
        return localCoordinates;
    }

    [Server]
    public void InstiateBattler(BattlerRef newBattler, int team){
        newBattler.pos = TransformCoordinates(newBattler.pos, team);
        GameObject go = Instantiate(battlerPrefab);
        NetworkServer.Spawn(go);
        Battler battler = go.GetComponent<Battler>();

        // Carrega a mesh no server e no client
        go.GetComponent<MeshFilter>().mesh = null;
        battler.RpcLoadMesh(newBattler.character.meshName);

        // Carrega as outras informacoes do battler
    }
}
