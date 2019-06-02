using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace HeroesOfCastella
{
    

    [System.Serializable]
    public class Battler : IBattler
    {
        [System.Serializable]
        private struct BattlerSerialized
        {
            //Use serializable fields to describe the battler before effectively serializing to a byte stream
        }

        [SerializeField]
        private float initiative;
        [SerializeField]
        public Character character;
        [SerializeField]
        private Vector3 position;
        private int team;
        [SerializeField]
        private bool active = false;



        private event EventHandler MyOnActionChosen;


        public void InitializeInitiative()
        {
            initiative = character.attributes.initiative;
            //ITurnManager t = new TurnManager(new List<IBattler>());
            //t.OnBattlerTurn += OnBattlerTurn;
        }



        event EventHandler IBattler.OnActionChosen
        {
            add
            {
                MyOnActionChosen += value;
            }

            remove
            {
                MyOnActionChosen -= value;
            }
        }

        public float GetInitiative()
        {
            //myOnActionChosen?.Invoke(this, EventArgs.Empty); //TODO remove

            return initiative;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public bool IsReady()
        {
            return initiative >= 12; //TODO use const
        }

        //Test
        public class MyEventArgs : EventArgs
        {
            public Action action;
            public MyEventArgs(Action action)
            {
                this.action = action;
            }
        }

        public void SubscribeToOnTurnStart(ref OnTurnStartDelegate e)
        {
            e += OnBattlerTurn;
        }

        //Event: it is some battler's turn
        private void OnBattlerTurn(IBattler battler)
        {
            //TODO unbind from TurnManager implementation
            IBattler actor = battler;
            Debug.Log("Battler " + this.GetName() + " knows it is " + actor.GetName() + "'s turn.");
            if (actor != this)
            {
                //do something?
                return;
            }
            //It is my turn
            active = true;
            //Get input
            //If action is legal, set active false ?
            // Action action = new Action(character.skills[0], this, Vector3.zero); //FIXME mock
            // MyOnActionChosen?.Invoke(this, new MyEventArgs(action)); //FIXME mock
            active = false; //FIXME remove this - must check if chosen action is legal before setting active to false
        }

        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }

        public int TakeDamage(int damage)
        {
            Debug.Log("Damage taken: " + damage); //TODO implement
            return damage; // Should return real damage taken instead
        }

        public void UpdateInitiative()
        {
            initiative += (5 + character.attributes.initiative) / 5f;
        }

        public int GetTeam()
        {
            return team;
        }

        public void SetTeam(int team)
        {
            this.team = team;
        }

        public void SetInitiative(float value)
        {
            initiative = value;
        }

        public byte[] Serialized()
        {
            //Format to a struct (with serializable data only) then use binary formatter to serialize
            throw new NotImplementedException();
        }

        public void Deserialize(byte[] data)
        {
            //Use binary formatter to deserialize to a struct then "build" the object from the struct's fields
            throw new NotImplementedException();
        }

        public bool IsActive()
        {
            return active;
        }

        public void SetActive(bool value)
        {
            active = value;
        }

        public string GetName()
        {
            return character.name;
        }

        
    }
}

