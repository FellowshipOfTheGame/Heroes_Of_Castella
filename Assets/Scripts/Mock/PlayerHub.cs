using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroesOfCastella;

namespace Mock
{
    public class PlayerHub : MonoBehaviour
    {
        BattleScene battleScene;
        public List<BattlerSO> battlerSOs;
        [SerializeField]
        List<IBattler> team = new List<IBattler>();

        private void Awake()
        {
            for (int i = 0; i < battlerSOs.Count; i++)
            {
                battlerSOs[i] = Instantiate(battlerSOs[i]);
            }
            battleScene = FindObjectOfType<BattleScene>();
            foreach(BattlerSO b in battlerSOs)
            {
                team.Add(b.battler);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            battleScene.AddTeam(team);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetPositions()
        {
            List<Vector3Int> availablePositions = new List<Vector3Int>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    availablePositions.Add(new Vector3Int(i, j, 0));
                }
            }
            foreach (IBattler b in team)
            {
                int rand = Random.Range(0, availablePositions.Count);
                Vector3Int pos = availablePositions[rand];
                availablePositions.RemoveAt(rand);
                Debug.Log("Position from PlayerHub: " + pos);
                b.Position = pos;
            }
        }
    }
}

