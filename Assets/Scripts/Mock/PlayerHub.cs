using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroesOfCastella;

namespace Mock
{
    public class PlayerHub : MonoBehaviour
    {
        BattleScene battleScene;
        [SerializeField]
        List<IBattler> team = new List<IBattler>();
        private List<Battler.InitializationParams> teamParams = new List<Battler.InitializationParams>();
        public List<CharacterBattlerParamsSO> paramsSOs = new List<CharacterBattlerParamsSO>();

        private void Awake()
        {
            battleScene = FindObjectOfType<BattleScene>();

            //for (int i = 0; i < battlerSOs.Count; i++)
            //{
            //    battlerSOs[i] = Instantiate(battlerSOs[i]);
            //}
            //foreach(BattlerSO b in battlerSOs)
            //{
            //    team.Add(b.battler);
            //}

            foreach (CharacterBattlerParamsSO p in paramsSOs)
            {
                Battler.InitializationParams par;
                par.character = p.characterSO.character;
                par.position = p.position;
                par.brain = p.brainSO.brain;
                teamParams.Add(par);
            }

            foreach (Battler.InitializationParams p in teamParams)
            {
                team.Add(new Battler(p));
            }

            // TODO REMOVE ALL LOOP CODE
            foreach (Battler b in team)
            {
                b.Deserialize(b.Serialized());
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

