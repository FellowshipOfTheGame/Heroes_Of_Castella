﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace HeroesOfCastella
{
    [System.Serializable]
    public class Brain_Random : IBrain
    {
        Battler battler;
        IBattleMap battleMap;

        public event OnActionChosenDelegate OnActionChosen;

        public void Initialize(ITurnTaker turnTaker, IBattleMap battleMap)
        {
            this.battler = turnTaker as Battler;
            this.battleMap = battleMap;
        }

        public void ChooseAction()
        {
            int skillsCount = 0;
            if (battler.character.skills == null)
            {
                OnActionChosen(new Action(new Skill(), battler.ID, battler.Position)); //FIXME MOCK
                return;
            }
            skillsCount = battler.character.skills.Length;
            if (skillsCount == 0)
            {
                OnActionChosen(new Action(new Skill(), battler.ID, battler.Position)); //FIXME MOCK
                return;
            }
            AsyncChooseAction(); // Check if this code is right - It is! S2 =) \o/        
        }

        private async Task AsyncChooseAction()
        {
            await Task.Run(ChooseRandomAction);
        }

        private void ChooseRandomAction()
        {
            int skillsCount = battler.character.skills.Length;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Action action = new Action(battler.character.skills[0], battler.ID, battler.Position); // FIXME crappy initialization - use default constructor?
            bool actionChosen = false;
            UnityEngine.Debug.Log("Choosing random action");
            while (stopwatch.ElapsedMilliseconds < 2000 && !actionChosen) // FIXME inject wait time
            {
                if (stopwatch.ElapsedMilliseconds < 1500) // FIXME Remove: Async test
                    continue;
                UnityEngine.Debug.Log("Choosing...");
                int rand = ThreadSafeRandom.Range(0, skillsCount);
                Vector3 mapSize = (battleMap.GetSize());
                Vector3 randTargetPos = new Vector3(ThreadSafeRandom.Range(0, (int)mapSize.x), ThreadSafeRandom.Range(0, (int)mapSize.y));
                action = new Action(battler.character.skills[rand], battler.ID, new Vector3(randTargetPos.x, randTargetPos.y));
                Battler target = (Battler)battleMap.GetElementAt(randTargetPos);
                if (target == null)
                { // no battler on that position
                    UnityEngine.Debug.Log("No battler in that position");
                    continue;
                }
                UnityEngine.Debug.Log("Battler found");
                if ((action.skill as Skill).targetType == Skill.TargetType.ALLIES) // targeted at allies
                {
                    if (target.GetTeam() == battler.GetTeam())
                    {
                        actionChosen = true;
                    }
                }
                else // targeted at enemies
                {
                    if (target.GetTeam() != battler.GetTeam())
                    {
                        actionChosen = true;
                    }
                }
            }
            if (actionChosen)
            {
                UnityEngine.Debug.Log("------------------------------------------- Random action chosen");
                OnActionChosen(action);
            }
        }

        public BrainSerializable Serialized(){
            BrainSerializable serialized;
            serialized.type = BrainType.Brain_Random;

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

            battler = null; // References can't be serialized
            battleMap = null; 
            OnActionChosen = null; // Events can't be serialized
        }

    }
}

