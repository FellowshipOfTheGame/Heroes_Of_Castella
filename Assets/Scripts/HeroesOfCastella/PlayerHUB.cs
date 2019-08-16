using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace HeroesOfCastella
{
    public class PlayerHUB : NetworkBehaviour
    {
        public static PlayerHUB clientInstance = null;
        private Brain_Player currentBrain = null;

        public void Awake(){
            if(isClient && clientInstance == null)
                clientInstance = this;
        }

        [Client]
        public void BattlersSelected(Battler[] battlers){
            UISwitcher.instance.ClosePartySetup();
            // The size here has to be fixed at 3 for now
            byte[][] serializedBattlers = new byte[3][];
            for(int i = 0; i < battlers.Length; i++){
                serializedBattlers[i] = battlers[i].Serialized();
            }
            CmdAddStartingBattlers(serializedBattlers[0], serializedBattlers[1], serializedBattlers[2], battlers.Length);
        }

        [Command]
        public void CmdAddStartingBattlers(byte[] serializedBattler1, byte[] serializedBattler2, byte[] serializedBattler3, int nBattlers){
            IBattler[] battlerList = new IBattler[nBattlers];
            (battlerList[0] as Battler).Deserialize(serializedBattler1);
            (battlerList[1] as Battler).Deserialize(serializedBattler2);
            (battlerList[2] as Battler).Deserialize(serializedBattler3);
            // for(int i = 0; i < nBattlers; i++){
            //     (battlerList[i] as Battler).Deserialize(serializedBattlers[i]);
            // }
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
        public void TargetRequestDecision(NetworkConnection conn, uint battlerID){
            //Finds the current battler by ID and allows player to select an action
            UISwitcher.instance.OpenPlayerAction(battlerID);
        }

        [Client]
        public void ActionDecided(Action action){
            UISwitcher.instance.ClosePlayerAction();
            CmdSendDecision(action.Serialized());
        }

        [Command]
        public void CmdSendDecision(byte[] actionSerial){
            Action action = new Action(new Skill(), 0, new Vector3[0]);
            action.Deserialize(actionSerial);
            currentBrain.Chose(action);
            currentBrain = null;
        }
    }
}