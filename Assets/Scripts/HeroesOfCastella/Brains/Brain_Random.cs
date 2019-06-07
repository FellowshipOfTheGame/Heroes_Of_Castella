using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;


namespace HeroesOfCastella
{
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
                OnActionChosen(new Action(new Skill(), battler, battler.Position)); //FIXME MOCK
                return;
            }
            skillsCount = battler.character.skills.Length;
            if (skillsCount == 0)
            {
                OnActionChosen(new Action(new Skill(), battler, battler.Position)); //FIXME MOCK
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
            Action action = new Action(new Skill(), battler, battler.Position); // FIXME crappy initialization - use default constructor?
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
                action = new Action(battler.character.skills[rand], battler, new Vector3(randTargetPos.x, randTargetPos.y));
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

    }
}

