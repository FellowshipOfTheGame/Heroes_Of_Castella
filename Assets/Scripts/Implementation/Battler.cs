using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella
{
    public class Battler<T> : IBattler where T: TurnManager.TurnEventArgs
    {
        private float initiative;
        private Character character;



        private event EventHandler MyOnActionChosen;


        public Battler()
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
            throw new NotImplementedException();
        }

        public bool IsReady()
        {
            throw new NotImplementedException();
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

        public void OnBattlerTurn(System.Object sender, EventArgs e)
        {
            //TODO unbind from TurnManager implementation
            IBattler actor = (e as T).battler;
            if (actor != this)
            {
                //do something?
                return;
            }
            Action action = new Action(character.skills[0], this, Vector3.zero);
            MyOnActionChosen?.Invoke(this, new MyEventArgs(action));
        }

        public void SetPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public int TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public void UpdateInitiative()
        {
            initiative += (5 + character.attributes.initiative) / 5f;
        }
    }
}

