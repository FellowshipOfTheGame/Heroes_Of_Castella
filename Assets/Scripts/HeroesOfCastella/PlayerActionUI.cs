using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesOfCastella{
    public class PlayerActionUI : MonoBehaviour
    {
        public delegate void TargetSelectionDelegate(Vector3 pos); // Might make more sense to declare this on the map script

        private BattlerBHV currentBattler = null;
        private int skillIndex = -1;

        public void SetupDecision(uint battlerID){
            // Find reference to current battler from the ID and save it
            currentBattler = FindObjectOfType<BattleMapBHV>().FindBattler(battlerID);
            // Indicate that the battler is selected
            // Call ShowBattlerSkills
        }

        private void ShowBattlerSkills(){
            // Show buttons for the current battler skills
        }

        // Each skill button should have this as a callback and set their index as parameter to the delegate
        public void SkillSelected(int index){
            // Save the skill index
            skillIndex = index;
            // Check which skill was selected
            ISkill skill = currentBattler.Skills[skillIndex];
            // Ask the map for the possible targets of this skill
            List<Vector3> possibleTargets = FindObjectOfType<BattleMapBHV>().GetSkillTargets(currentBattler, skill);
            // Make all the targets clickable (maybe this should be done by the map)
            // Deactivate the buttons for skill selection
            // Activate button for cancelling target selection (maybe)
        }

        // Callback for the cancel button
        public void CancelSkillSelection(){
            skillIndex = -1;
            // Revert the changes of skill selected
            // Call ShowBattlerSkills
        }

        // Buttons for target selection should have this method as delegate, with their position as parameter
        public void TargetSelected(Vector3 pos){
            // Deactivate the clickable target components (Might be a map function)
            // Deactivate the cancel button
            Action action = new Action(currentBattler.Skills[skillIndex], currentBattler.ID, pos);
            PlayerHUB.clientInstance.ActionDecided(action);
            currentBattler = null;
            skillIndex = -1;
        }
    }
}