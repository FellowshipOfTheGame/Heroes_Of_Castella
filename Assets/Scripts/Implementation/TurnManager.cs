using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Threading.Tasks;

namespace HeroesOfCastella
{
    public class TurnManager : ITurnManager //TODO inherit from NetworkBehaviour?
    {
        //event EventHandler OnBattlerTurn;
        private event OnTurnStartDelegate myOnBattlerTurn;
        private bool locked = true;
        private ITurnTaker activeBattler;

        private List<ITurnTaker> _turnTakers = new List<ITurnTaker>();
        public List<ITurnTaker> TurnTakers {
            get {
                return _turnTakers;
            }
            set {
                if (_turnTakers.Count > 0)
                {
                    //TODO implement
                    throw new NotImplementedException();
                    //remove listeners, clear list, etc
                }
                _turnTakers = value;
                foreach (ITurnTaker b in _turnTakers)
                {
                    //myOnBattlerTurn += b.OnBattlerTurn; //Each battler will listen when it is someone's turn
                    b.SubscribeToOnTurnStart(ref myOnBattlerTurn);
                    //b.OnActionChosen += OnBattlerActionChosen; //Turn Manager will listen when a battler has chosen an action //Removed: turn manager should know when the action is completed, instead
                }
            }
        } // = new List<ITurnTaker>();

        event OnTurnStartDelegate ITurnManager.OnBattlerTurn
        {
            add
            {
                myOnBattlerTurn += value;
            }

            remove
            {
                myOnBattlerTurn -= value;
            }
        }

        public TurnManager()
        {

        }

        //private List<ITurnTaker> SetBattlers(List<ITurnTaker> battlers)
        //{
        //    if (this.TurnTakers.Count > 0)
        //    {
        //        //TODO implement
        //        throw new NotImplementedException();
        //        //remove listeners, clear list, etc
        //    }
        //    // this.TurnTakers = battlers;
        //    foreach (ITurnTaker b in battlers)
        //    {
        //        //myOnBattlerTurn += b.OnBattlerTurn; //Each battler will listen when it is someone's turn
        //        b.SubscribeToOnTurnStart(ref myOnBattlerTurn);
        //        //b.OnActionChosen += OnBattlerActionChosen; //Turn Manager will listen when a battler has chosen an action //Removed: turn manager should know when the action is completed, instead
        //    }
        //    return battlers;
        //}


        public void Update()
        {
            // Debug.Log("Update() on TurnManager");
            if (locked)
                return;
            //Delegate to queue
            foreach(ITurnTaker b in TurnTakers)
            {
                //It is some battler's turn
                if (b.IsReady())
                {
                    locked = true;
                    activeBattler = b;
                    myOnBattlerTurn?.Invoke(b);
                    //Run timer (thread?) to unlock when it's over
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    DelayedUnlock(3000); // TODO use coroutine if threading f**ks up
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    return; // could return some other value (IEnumerator even) for a wrapper class to start the coroutine
                }
            }
            foreach (ITurnTaker b in TurnTakers)
            {
                b.UpdateInitiative();
            }
        }

        public void Lock() //Just setting this variable won't suffice: required to set a forced lock so that the Turn Manager won't unlock itself
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }

        //public void OnBattlerTurnEnded(System.Object sender, EventArgs e)
        //{
        //    locked = false;
        //}

        private async Task DelayedUnlock(int miliseconds)
        {
            await Task.Delay(miliseconds);
            activeBattler.SetInitiative(0);
            Unlock();
        }

        public void RemoveBattler(ITurnTaker battler)
        {
            battler.UnSubscribeToOnTurnStart(ref myOnBattlerTurn);
            TurnTakers.Remove(battler);
        }
    }
}

