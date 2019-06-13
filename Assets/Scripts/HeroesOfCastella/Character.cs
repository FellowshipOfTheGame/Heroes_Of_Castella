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

        [System.Serializable]
        public struct Serializable
        {
            public string name;
            public Attributes attributes;
            public Personality personality;
            public string[] skillNames;
        }

        public string name = "NEMO";
        public Attributes attributes;
        public Personality personality;
        public Skill[] skills;


        public Character(byte[] data)
        {
            Deserialize(data);
        }

        public byte[] Serialized()
        {
            Serializable data = new Serializable();
            data.name = name;
            data.attributes = attributes;
            data.personality = personality;
            data.skillNames = new string[skills.Length];
            for(int i = 0; i < skills.Length; i++)
            {
                data.skillNames[i] = skills[i].name;
            }

            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, data);
            return stream.GetBuffer();
        }

        public void Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Serializable s = (Serializable)binaryFormatter.Deserialize(stream);

            attributes = s.attributes;
            personality = s.personality;
            name = s.name;
            skills = new Skill[s.skillNames.Length];
            for (int i = 0; i < s.skillNames.Length; i++)
            {
                skills[i] = (Skill)Resources.Load("Assets/Data/Skills/" + s.skillNames[i]);
            }
            //FIXME remove bellow
            //skills = new Skill[1];
            //skills[0] = new Skill();
        }
    }
}

