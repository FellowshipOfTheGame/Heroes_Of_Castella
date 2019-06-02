using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella
{
    public class BattleMapBHV : MonoBehaviour
    {
        IBattleMap map;
        bool initialized = false;
        public BattlerBHV battlerPrefab;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
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
                        Vector3 pos = b.GetPosition();
                        pos = new Vector3(pos.x, 0, pos.y);
                        pos = (pos - new Vector3(5 / 2f, 0f, 2 / 2f)) * 2 + new Vector3((t-1.5f)*1, 0);
                        Quaternion rot = Quaternion.LookRotation(Vector3.back * (t - 1.5f));
                        BattlerBHV obj = Instantiate(battlerPrefab, pos, rot, transform);
                        obj.name = "T" + t + "_B" + bt;
                    }
                }
            }
        }

        public void SetMap(IBattleMap map)
        {
            this.map = map;
        }

    }
}

