using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


    public abstract class CompanyCharacterController : MonoBehaviour
    {

        [SerializeField] protected CharacterStatsDefinition characterStatsDefinition;
        protected CharacterMutableModel model;
        public CharacterMutableModel Model
        {
            get => model;
            set => model = value;
        }

        public void Setup(CharacterMutableModel model)
        {
            this.model = model;
        }

        public virtual bool TryAddEffect(EffectDefinition effectDefinition, CompanyCharacterController effectOwner, bool isFromTree = false, int index = -1)
        {
            return model.TryAddEffect(effectDefinition, isFromTree, index);
        }

        public virtual bool TryRemoveEffect(EffectDefinition effectDefinition)
        {
            return model.TryRemoveEffect(effectDefinition);
        }

        public virtual void ApplyDamage(Strike strike)
        {
            Debug.Log("Health before damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
            bool isAlive = model.ApplyDamage(strike);
            Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
            Debug.Log("Health after damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

            if (!isAlive)
            {
                Debug.Log("TODO -> Behaviour when dies. Maybe should override method if enemy or player");
            }
            
        }

        public virtual Stat GetStatFromName(StatNames statName)
        {
            return model.GetStatFromName(statName);
        }

        public virtual int GetStatValue(StatNames statName, StatParts statPart)
        {
            if(model == null)
            {
                Debug.Log("NULL");
            }
            return model.GetStatValue(statName, statPart);
        }

        public virtual DuringTimeEffect GetDuringTimeEffectByType(EffectTypesEnum effectType)
        {
            return model.GetDuringTimeEffectByType(effectType);
        }
        public virtual OverTimeEffect GetOverTimeEffectByType(EffectTypesEnum effectType)
        {
            return model.GetOverTimeEffectByType(effectType);
        }

        public virtual void ChangeActualStatInstantly(EffectDefinition effectDefinition)
        {
            model.ChangeActualStat(effectDefinition);
        }

        public virtual void ChangeActualMaxStatInstantly(EffectDefinition effectDefinition)
        {
            model.ChangeActualMaxStat(effectDefinition);
        }
    }


