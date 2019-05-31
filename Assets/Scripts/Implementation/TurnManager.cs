using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeroesOfCastella
{
    public class TurnManager : ITurnManager
    {
        event EventHandler OnBattlerTurn;
        private event EventHandler myOnBattleTurn;
        private List<IBattler> battlers = new List<IBattler>();


        public class TurnEventArgs : EventArgs
        {
            public IBattler battler;
            public TurnEventArgs(IBattler battler)
            {
                this.battler = battler;
            }
        }

        event EventHandler ITurnManager.OnBattlerTurn
        {
            add
            {
                myOnBattleTurn += value;
            }

            remove
            {
                myOnBattleTurn += value;
            }
        }

        public TurnManager (List<IBattler> battlers)
        {
            this.battlers = battlers;
            foreach (IBattler b in battlers)
            {
                //b.OnActionChosen += OnBattlerActionChosen;
                OnBattlerTurn += b.OnBattlerTurn;
            }
        }

        

        public void Update()
        {
            //Delegate to queue
            foreach(IBattler b in battlers)
            {
                if (b.IsReady())
                {
                    OnBattlerTurn?.Invoke(this, new TurnEventArgs(b));
                    //b.OnBattlerTurn(b); // not how its supposed to be: an event, instead
                    return;
                }
            }
            foreach (IBattler b in battlers)
            {
                b.UpdateInitiative();
            }
        }

        //void OnBattlerActionChosen(System.Object sender, EventArgs e)
        //{

        //}
    }
}

