using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HeroesOfCastella
{
    [System.Serializable]
    public class Brain_Player : IBrain
    {
        private PlayerHUB player = null;
        public int battlerID { get; protected set; }
        
        public event OnActionChosenDelegate OnActionChosen;

        public void Initialize(ITurnTaker turnTaker, IBattleMap battleMap){
            battlerID = (turnTaker as Battler).ID;
        }

        public void ChooseAction(){
            player.RequestDecision(this);
        }

        public void Configure(PlayerHUB hub){
            player = hub;
        }

        public void Chose(Action action){
            OnActionChosen(action);
        }

        public BrainSerializable Serialized(){
            BrainSerializable serialized;
            serialized.type = BrainType.Brain_Player;

            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, this);
            serialized.data = stream.GetBuffer();

            return serialized;
        }

        public void Deserialize(byte[] data){
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Brain_Player obj = binaryFormatter.Deserialize(stream) as Brain_Player;

            battlerID = obj.battlerID;
            OnActionChosen = null; // Events can't be serialized
        }
    }
}