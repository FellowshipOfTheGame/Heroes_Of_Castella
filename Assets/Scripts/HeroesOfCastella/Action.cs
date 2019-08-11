using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HeroesOfCastella
{
    [System.Serializable]
    public class Action : IAction, ISerializable
    {
        public Skill skill;
        public uint battlerID;
        public Vector3[] target;

        public Action (ISkill skill, uint battlerID, params Vector3[] target)
        {
            this.skill = skill as Skill;
            this.battlerID = battlerID;
            this.target = target;
        }

        public byte[] Serialized(){
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, this);
            return stream.GetBuffer();
        }

        public void Deserialize(byte[] data){
            //Use binary formatter to deserialize to a struct then "build" the object from the struct's fields
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Action dataAction = (Action)binaryFormatter.Deserialize(stream);
            skill = dataAction.skill;
            battlerID = dataAction.battlerID;
            target = new Vector3[dataAction.target.Length];
            dataAction.target.CopyTo(target, 0);
        }
    }
}

