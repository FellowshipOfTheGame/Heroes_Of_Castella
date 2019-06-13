using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace HeroesOfCastella
{
    [System.Serializable]
    public class Character : ISerializable
    {
        [System.Serializable]
        public struct Attributes {
            public int strength;
            public int dexterity;
            public int spirit;
            public int vitality;
            public int initiative;
        }

        [System.Serializable]
        public struct Personality
        {
            [Range(-1f, 1f)]
            public float empathy;
            [Range(-1f, 1f)]
            public float agressivity;
        }

        public string name = "NEMO";
        public Attributes attributes;
        public Personality personality;
        public Skill[] skills;

        public byte[] Serialized()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, this);
            return stream.GetBuffer();
        }

        public void Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Character c = (Character)binaryFormatter.Deserialize(stream);
            attributes = c.attributes;
            personality = c.personality;
            name = c.name;
            //FIXME remove bellow
            skills = new Skill[1];
            skills[0] = new Skill();
        }
    }
}

