using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace HeroesOfCastella
{
    public class BattleMapBHV : NetworkBehaviour
    {
        IBattleMap map;
        bool initialized = false;
        public BattlerBHV battlerPrefab;
        private List<BattlerBHV> battlers = new List<BattlerBHV>();

        // Start is called before the first frame update
        public override void OnStartServer()
        {
            base.OnStartServer();
            GameObject.FindObjectOfType<BattleScene>().onBattleInitialize += CreateBattlerBHVs;
        }

        [Server]
        public void CreateBattlerBHVs(){
            if (map != null && !initialized)
            {
                initialized = true;
                List<List<IBattler>> teams = map.GetBattlerTeams();
                int t = 0;
                foreach(List<IBattler> team in teams)
                {
                    t++;
                    int bt = 0;
                    foreach(IBattler b in team)
                    {
                        bt++;
                        Vector3 pos = b.Position;
                        pos = new Vector3(pos.x, 0, pos.y);
                        pos = (pos - new Vector3(5 / 2f, 0f, 2 / 2f)) * 2 + new Vector3((t-1.5f)*1, 0);
                        pos.z *= 1.5f;
                        Quaternion rot = Quaternion.LookRotation(Vector3.back * (t - 1.5f));
                        BattlerBHV obj = Instantiate(battlerPrefab, pos, rot, transform.GetChild(0));
                        obj.Watch(b as Battler);
                        obj.name = "T" + t + "_B" + bt;
                        NetworkServer.Spawn(obj.gameObject);
                    }
                }
            }
        }

        [Client]
        public void AddBattlerBHV(BattlerBHV battlerBHV){
            battlers.Add(battlerBHV);
            // Maybe set the object as child? Need to check how it's happening now
        }

        // Update is called once per frame
        void Update()
        {
            // Reads every BattlerBHV Position property,
            // determines their world position and moves them accordingly
        }

        [Client]
        public BattlerBHV FindBattler(uint battlerID){
            BattlerBHV battler = null;
            foreach(BattlerBHV b in battlers){
                if(b.ID == battlerID){
                    battler = b;
                    break;
                }
            }
            return battler;
        }

        [Client]
        public List<Vector3> GetSkillTargets(BattlerBHV battler, ISkill skill){
            List<Vector3> targets = new List<Vector3>();
            // Checks the current state of the map and the position of all battlers
            // Adds all positions that count as valid targets for the skill to the list
            return targets;
        }

        [Server]
        public void SetMap(IBattleMap map)
        {
            this.map = map;
        }

    }
}

