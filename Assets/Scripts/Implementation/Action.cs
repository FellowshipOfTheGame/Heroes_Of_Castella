using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella
{
    public class Action : IAction
    {
        public ISkill skill;
        public IBattler battler;
        public Vector3[] target;

        public Action (ISkill skill, IBattler battler, params Vector3[] target)
        {
            this.skill = skill;
            this.battler = battler;
            this.target = target;
        }
    }
}

