using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella{
    public class UISwitcher : MonoBehaviour
    {
        // Singleton
        public static UISwitcher instance = null;
        public void Awake(){
            if(instance == null)
                instance = this;
            else
                Destroy(this);

            // Save reference to the UI game objects
            if(partySetupObj == null)
                partySetupObj = FindObjectOfType<PartySetupUI>().gameObject;
            if(playerActionObj == null)
                playerActionObj = FindObjectOfType<PlayerActionUI>().gameObject;

            // Save reference to the relevant scripts in those game objects
            if(partySetupObj != null)
                PartySetupUI = partySetupObj.GetComponent<PartySetupUI>();
            if(playerActionObj != null)
                PlayerActionUI = playerActionObj.GetComponent<PlayerActionUI>();
        }

        public PartySetupUI PartySetupUI { get; private set; }
        public PlayerActionUI PlayerActionUI { get; private set; }

        [SerializeField]
        private GameObject partySetupObj;
        [SerializeField]
        private GameObject playerActionObj;

        public void OpenPartySetup(){
            if(partySetupObj){
                partySetupObj.SetActive(true);
            }
        }

        public void ClosePartySetup(){
            if(partySetupObj)
                partySetupObj.SetActive(false);
        }

        public void OpenPlayerAction(uint battlerID){
            if(playerActionObj){
                playerActionObj.SetActive(true);
                if(PlayerActionUI)
                    PlayerActionUI.SetupDecision(battlerID);
            }
        }

        public void ClosePlayerAction(){
            if(playerActionObj)
                playerActionObj.SetActive(false);
        }
    }
}
