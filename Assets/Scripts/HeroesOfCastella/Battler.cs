using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace HeroesOfCastella
{

    
    [System.Serializable]
    public class Battler : IBattler
    {
        [System.Serializable]
        private struct BattlerSerializable
        {
            //Use serializable fields to describe the battler before effectively serializing to a byte stream
            //public Byte[] serializedCharacter;
            //public string brainType;
            //public IBrain serializedBrain;
            //public float posX;
            //public float posY;


            public Byte[] serializedCharacter;

            public Character.Attributes attributes;
            public Character.Personality personality;
            public string[] skillNames;
            public IBrain brain;
            public float posX;
            public float posY;

        }

        [System.Serializable]
        public struct InitializationParams
        {
            public Character character;
            public IBrain brain;
            public Vector3 position;
        }

        [SerializeField]
        private float initiative;
        [SerializeField]
        public Character character;
        [SerializeField]
        private Vector3 _position; // TODO remove, probably
        private int team;
        [SerializeField]
        private bool active = false;

        [SerializeField]
        private IBrain brain = new Brain_Random(); // TODO: Make it an SO, so that it can be more freely swapped?

       
        public Vector3 Position { get => _position; set => _position = value; }
        public IBattleMap Map { get; set; }

        [SerializeField]
        private int _hitpoints = 100; //FIXME MOCK
        public int HP { get => _hitpoints; set => _hitpoints = value; }

        private event OnActionChosenDelegate MyOnActionChosen;


        public Battler(InitializationParams p)
        {
            character = p.character;
            Position = p.position;
            if (p.brain != null)
            {
                brain = p.brain;
            }
        }


        public void InitializeInitiative()
        {
            initiative = character.attributes.initiative;
            //ITurnManager t = new TurnManager(new List<IBattler>());
            //t.OnBattlerTurn += OnBattlerTurn;
        }

        public void InitializeBattler()
        {
            //Set hp and whatever else
            brain.Initialize(this, Map);
            brain.OnActionChosen += OnBrainActionChosen; // listen to own brain
            if (character.skills.Length == 0) //FIXME remove this code - MOCK
            {
                character.skills = new Skill[1];
                character.skills[0] = new Skill();
            }
        }




        public event OnActionChosenDelegate OnActionChosen
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

        public void UnSubscribeToOnTurnStart(ref OnTurnStartDelegate e)
        {
            e -= OnBattlerTurn;
        }

        //Event: it is some battler's turn
        private void OnBattlerTurn(ITurnTaker battler)
        {
            IBattler actor = battler as IBattler;
            Debug.Log("Battler " + this.GetName() + " knows it is " + actor.GetName() + "'s turn.");
            if (actor != this)
            {
                //do something?
                return;
            }
            Debug.Log(this.GetName() + " is choosing his action.");
            //It is my turn
            active = true;
            //Get input
            //If action is legal, set active false ?
            // Action action = new Action(character.skills[0], this, Vector3.zero); //FIXME mock
            // MyOnActionChosen?.Invoke(this, new MyEventArgs(action)); //FIXME mock
            // Battler => Brain => PlayerHub:Server => PlayerHub:Client => HUD -> PlayerHub:Client -> PlayerHub:Server -> Brain -> Battler
            brain.ChooseAction(); // brain will choose an action to take and trigger an event once it is chosen

            //OnBrainActionChosen(new Action(new Skill(), this, Position)); //FIXME Mock

            // deactivation of battler should happen when it gets an event telling that the brain's choice of action was accepted
            active = false; //FIXME remove this - must check if chosen action is legal before setting active to false.
        }

        private void OnBrainActionChosen(IAction action)
        {
            // MyOnActionChosen?.Invoke(action); // Should there be an event for this?
            Action act = action as Action;
            act.skill.Apply(this, act.target);
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
            BattlerSerializable data = new BattlerSerializable();
            data.serializedCharacter = character.Serialized();
            data.brain = brain;
            data.posX = Position.x;
            data.posY = Position.y;

            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, data);
            return stream.GetBuffer();
        }

        public void Deserialize(byte[] data)
        {
            //Use binary formatter to deserialize to a struct then "build" the object from the struct's fields
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            BattlerSerializable obj = (BattlerSerializable)binaryFormatter.Deserialize(stream);
            Character c = new Character();
            c.Deserialize(obj.serializedCharacter);
            character = c;
            //IBrain b = (IBrain)Activator.CreateInstance(Type.GetType(obj.brainType));
            //brain = b;
            brain = obj.brain;
            Position = new Vector3(obj.posX, obj.posY);
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

