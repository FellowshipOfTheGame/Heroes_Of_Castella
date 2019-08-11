using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using Mirror;


namespace HeroesOfCastella
{
    public class BattlerBHV : NetworkBehaviour
    {
        public Battler battler;
        private List<ISkill> skills;

        // These syncvars contain information relevant for visual feedback
        [SyncVar] private SyncListString skillNames = new SyncListString();
        [SyncVar] private Vector3 position;
        [SyncVar] private uint id;
        [SyncVar] private int curHP;
        [SyncVar] private int maxHP;

        // These public variables and properties contain the information visible from the outside
        public ReadOnlyCollection<ISkill> Skills{ get{ return skills.AsReadOnly(); } }
        public uint ID { get => id; set{ if(isServer) id = value; } }
        public int CurHP { get => curHP; }
        public int MaxHP { get => maxHP; }

        // Start is called before the first frame update
        void Start()
        {

        }

        [Server]
        public void Watch(Battler battler){
            this.battler = battler;
            id = battler.ID;
            curHP = battler.HP;
            maxHP = battler.HP;
            position = battler.Position;
            skills = new List<ISkill>(battler.character.skills);
            for(int i = 0; i < skills.Count; i++){
                skillNames.Add((skills[i] as Skill).name);
            }
            // Save any other necessary references, suchs as equipments
        }

        public override void OnStartClient(){
            base.OnStartClient();
            skills = new List<ISkill>();
            for(int i = 0; i < skillNames.Count; i++){
                skills.Add(AssetManager.LoadAsset<Skill>(skillNames[i]));
            }
            // Instantiate visual components of the battlerBHV based on the syncvars
        }

        public void Animate(string triggerName){
            GetComponent<Animator>().SetTrigger(triggerName);
            if(isServer)
                GetComponent<NetworkAnimator>().SetTrigger(triggerName);
        }

        public void Animate(string variable, bool state){
            GetComponent<Animator>().SetBool(variable, state);
        }

        // Update is called once per frame
        [ServerCallback]
        void Update()
        {
            // Verify if any changes ocurred on the battler and update the syncvars if necessary
        }
    }
}

