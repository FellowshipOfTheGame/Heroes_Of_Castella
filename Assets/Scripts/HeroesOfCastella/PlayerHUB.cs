using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace HeroesOfCastella
{
    public class PlayerHUB : NetworkBehaviour
    {
        Brain_Player currentBrain = null;

        [Command]
        public void CmdAddStartingBattlers(byte[] serializedBattler1, byte[] serializedBattler2, byte[] serializedBattler3){
            IBattler[] battlerList = new IBattler[3];
            battlerList[0].Deserialize(serializedBattler1);
            battlerList[1].Deserialize(serializedBattler2);
            battlerList[2].Deserialize(serializedBattler3);
            foreach(Battler battler in battlerList){
                battler.Configure(this);
            }

            FindObjectOfType<BattleScene>().AddTeam(new List<IBattler>(battlerList));
        }

        [Server]
        public void RequestDecision(Brain_Player brain){
            currentBrain = brain;
            TargetRequestDecision(connectionToClient, brain.battlerID);
            //TODO make this decision have a time limit
        }

        [TargetRpc]
        public void TargetRequestDecision(NetworkConnection conn, int battlerID){
            //TODO Finds the current battler by ID and allows player to select an action (or tells someone to do it, which will probably be the case)
            Action newAction = new Action(new Skill(), battlerID, new Vector3[0]);
            CmdSendDecision(newAction.Serialized()); // FIXME ; MOCK : need to define how this decision will be done
        }

        [Command]
        public void CmdSendDecision(byte[] actionSerial){
            Action action = new Action(new Skill(), 0, new Vector3[0]);
            action.Deserialize(actionSerial);
            currentBrain.Chose(action);
        }
    }
}