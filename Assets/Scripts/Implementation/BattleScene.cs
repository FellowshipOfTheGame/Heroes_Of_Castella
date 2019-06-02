using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;


namespace HeroesOfCastella
{
    public class BattleScene : MonoBehaviour //FIXME use NetworkBehaviour instead
    {
        ITurnManager turnManager = new TurnManager();
        IBattleMap battleMap = new BattleMap();
        int nTeams = 0;
        const int MAX_TEAMS = 2;
        List<List<IBattler>> teams = new List<List<IBattler>>();
        bool isReady = false;


        private void Awake()
        {
            
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
                }
            }
            turnManager.SetBattlers(battlers);
            isReady = true;
        }

        public bool IsReady() //TODO should probably use an event instead
        {
            return isReady;
        }

        public void Update()
        {
            turnManager.Update();
        }

        void OnBattlerActionChosen(System.Object sender, EventArgs e)
        {
            //Make sure the action happens
            IAction action = (e as Battler.MyEventArgs).action; //TODO find a way to fix these template stuff
            //Execute action
            //Do something if action is invalid
            //Then...
            turnManager.Unlock(); //lets the TurnManager carry on - battler action was executed
        }

    }
}

