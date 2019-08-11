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
        public void BattlersSelected(Battler battler1, Battler battler2, Battler battler3){
            UISwitcher.instance.ClosePartySetup();
            CmdAddStartingBattlers(battler1.Serialized(), battler2.Serialized(), battler3.Serialized());
        }

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