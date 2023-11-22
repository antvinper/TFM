using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    [CreateAssetMenu(fileName = "Skill Definition", menuName = "Skills/Skill Definition")]
    public class SkillDefinition : ScriptableObject
    {
        //public new PlayerEnumSkills name;
        public new string name;
        [TextArea] public string description;
        public List<InstantEffectTemporallyDefinition> temporalInstantEffects = new List<InstantEffectTemporallyDefinition>();
        public List<InstantEffectPermanentDefinition> permanentInstantEffects = new List<InstantEffectPermanentDefinition>();
        public List<OverTimeEffect> overTimeEffects = new List<OverTimeEffect>();
        public List<DuringTimeEffect> duringTimeEffects = new List<DuringTimeEffect>();

        public async Task ProcessSkill(CompanyCharacterController owner, CompanyCharacterController target)
        {
            ProcessTemporalInstantEffects(owner, target);
            ProcessDuringTimeEffects(owner, target);
            ProcessOverTimeEffects(owner, target);
        }

        public async Task RemoveSkillEffects(CompanyCharacterController target)
        {
            RemoveTemporalInstantEffects(target);
        }

        public async Task ProcessSkill(CompanyCharacterController target)
        {
            ProcessPermanentInstantEffects(target);
        }

        private void ProcessTemporalInstantEffects(CompanyCharacterController owner, CompanyCharacterController target)
        {
            foreach(InstantEffectTemporallyDefinition effect in temporalInstantEffects)
            {
                effect.ProcessEffect(owner, target);
            }
        }

        private void RemoveTemporalInstantEffects(CompanyCharacterController target)
        {
            foreach(InstantEffectTemporallyDefinition effect in temporalInstantEffects)
            {
                effect.RemoveEffect(target);
            }
        }
        /**
         * TODO
         * Move from here to another kind of skill definition.
         */
        private void ProcessPermanentInstantEffects(CompanyCharacterController target)
        {
            foreach (InstantEffectPermanentDefinition effect in permanentInstantEffects)
            {
                effect.ProcessEffect(target);
            }
        }

        private void ProcessOverTimeEffects(CompanyCharacterController owner, CompanyCharacterController target)
        {
            foreach (OverTimeEffect effect in overTimeEffects)
            {
                effect.ProcessEffect(owner, target);
            }
        }

        private void ProcessDuringTimeEffects(CompanyCharacterController owner, CompanyCharacterController target)
        {
            foreach (DuringTimeEffect effect in duringTimeEffects)
            {
                effect.ProcessEffect(owner, target);
            }
        }


    }
}

