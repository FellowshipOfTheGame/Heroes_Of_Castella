﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Windows;
using Mirror;
using System.Linq;


namespace HeroesOfCastella
{
    public class BattleScene : NetworkBehaviour
    {
        ITurnManager turnManager = new TurnManager();
        IBattleMap battleMap = new BattleMap();
        int nTeams = 0;
        const int MAX_TEAMS = 2;
        [SerializeField] //TODO remove
        List<List<IBattler>> teams = new List<List<IBattler>>();
        bool isReady = false;

        public delegate void VoidDelegate();
        public event VoidDelegate onBattleInitialize = null;

        


        private void Awake()
        {
            FindObjectOfType<BattleMapBHV>().SetMap(battleMap);
            //TODO remove below (test)
            //string[] scripts = AssetDatabase.FindAssets(filter: "", searchInFolders: new string[]{ "Assets/Scripts/Abstract" });
            //Debug.Log("--- Found scripts: ");
            //foreach (string s in scripts)
            //{
            //    Debug.Log(" - " + s);
            //}
            //object[] scripts = Resources.FindObjectsOfTypeAll(typeof(Battler));
            //foreach (object s in scripts)
            //{
            //    Debug.Log(" - " + s.GetType());
            //}
        }

        //PlayerHUB will request to add team
        public void AddTeam(List<IBattler> team)
        {
            foreach(IBattler b in team)
            {
                b.SetTeam(nTeams);
            }
            teams.Add(team);
            nTeams++;
            if (nTeams == MAX_TEAMS)
            {
                //Ready to start
                Initialize();
            }
        }

        private void Initialize()
        {
            //turnManager = new TurnManager(); // should construct from another class - DI (use public NetworkBehaviour on Inspector, maybe?)
            //battleMap = new BattleMap(); // should construct from another class - DI (use public NetworkBehaviour on Inspector, maybe?)
            List<IBattler> battlers = new List<IBattler>();
            foreach (List<IBattler> t in teams)
            {
                battlers.AddRange(t); // for the Turn Manager
                battleMap.AddTeam(t); // adds teams to Battle Map
                //Listen to battlers
                foreach (IBattler b in t)
                {
                    b.OnActionChosen += OnBattlerActionChosen;
                    b.InitializeBattler(); // TODO maybe find a better place to get it into
                }
            }
            //turnManager.SetBattlers(battlers.ToList<ITurnTaker>());
            turnManager.TurnTakers = battlers.ToList<ITurnTaker>();
            RpcCharacterSelectionEnded();
            onBattleInitialize();
            isReady = true;
            turnManager.Unlock();
        }

        [ClientRpc]
        public void RpcCharacterSelectionEnded(){
            // Destroy the ui elements for character selection
            // and instantiate elements required for gameplay
            FindObjectOfType<UISwitcher>().ClosePartySetup();
        }

        public bool IsReady() //TODO should probably use an event instead - check: what is it for?
        {
            return isReady;
        }

        public void Update()
        {
            turnManager.Update();
        }

        void OnBattlerActionChosen(IAction action) // TODO remove this; maybe TurnManager should listen to battler instead, case they are done
        {
            //Make sure the action happens
            // IAction action = (e as Battler.MyEventArgs).action; //TODO find a way to fix these template stuff
            //Execute action
            //Do something if action is invalid
            //Then...
            turnManager.Unlock(); //lets the TurnManager carry on - battler action was executed
        }

    }
}

