using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella
{
    public class BattleMap : IBattleMap
    {
        enum FrontalPos
        {
            BACK = 0,
            MID = 1,
            FRONT = 2
        }
        enum SidePos
        {
            RIGHT = 0,
            MID = 1,
            LEFT = 2
        }

        readonly Vector3 REF_POS = new Vector3(5,2);

        IBattler[,] battlers = new IBattler[6,3];

        List<List<IBattler>> teams = new List<List<IBattler>>();

        public event EventHandler OnBattlerDead;

        /*
         *       TEAM 0             TEAM 1                 TEAM 0             TEAM 1 
         * | 0,2 | 1,2 | 2,2 || 2,0 | 1,0 | 0,0 |    | 0,2 | 1,2 | 2,2 || 3,2 | 4,2 | 5,2 |
         * | 0,1 | 1,1 | 2,1 || 2,1 | 1,1 | 0,1 | => | 0,1 | 1,1 | 2,1 || 3,1 | 4,1 | 5,1 |
         * | 0,0 | 1,0 | 2,0 || 2,2 | 1,2 | 0,2 |    | 0,0 | 1,0 | 2,0 || 3,0 | 4,0 | 5,0 |
         */
        public bool AddBattler(IBattler battler, Vector3 position) //TODO remove from interface (?)
        {
            int team = battler.GetTeam();
            Vector3 pos = battler.GetPosition();
            //Adjust global position
            if (team != 0)
            {
                pos = REF_POS - pos;
                battler.SetPosition(pos);
            }
            //Place on the map
            Debug.Log("Battler original position: " + position);
            Debug.Log("Position on map: " + pos);
            battlers[(int)pos.x, (int)pos.y] = battler;
            //All went right? - Check for conflicts, out of range, etc.
            return true;
        }

        public bool AddTeam(List<IBattler> team)
        {
            teams.Add(team);
            foreach(IBattler b in team)
            {
                AddBattler(b, b.GetPosition());
            }
            return true;
        }

        public bool MoveBattlerTo(IBattler battler, Vector3 position)
        {
            throw new NotImplementedException();
        }

        public bool RemoveBattler(IBattler battler)
        {
            throw new NotImplementedException();
        }

        public float GetDistance(Vector3 v1, Vector3 v2)
        {
            throw new NotImplementedException();
        }

        public float GetWalkDistance(Vector3 v1, Vector3 v2)
        {
            throw new NotImplementedException();
        }

        public List<List<IBattler>> GetBattlerTeams()
        {
            return teams;
        }
    }
}

