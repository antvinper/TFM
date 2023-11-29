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


        public bool TryAddEffect(EffectDefinition effectDefinition, CompanyCharacterController effectOwner, bool isFromTree = false, int index = -1)
        {
            return model.TryAddEffect(effectDefinition, isFromTree, index);
        }

        public bool TryRemoveEffect(EffectDefinition effectDefinition)
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

        public Stat GetStatFromName(StatNames statName)
        {
            return model.GetStatFromName(statName);
        }

        public int GetStatValue(StatNames statName, StatParts statPart)
        {
            return model.GetStatValue(statName, statPart);
        }

        public DuringTimeEffect GetDuringTimeEffectByType(EffectTypesEnum effectType)
        {
            return model.GetDuringTimeEffectByType(effectType);
        }
        public OverTimeEffect GetOverTimeEffectByType(EffectTypesEnum effectType)
        {
            return model.GetOverTimeEffectByType(effectType);
        }

        public void ChangeActualStatInstantly(EffectDefinition effectDefinition)
        {
            model.ChangeActualStat(effectDefinition);
        }

        public void ChangeActualMaxStatInstantly(EffectDefinition effectDefinition)
        {
            model.ChangeActualMaxStat(effectDefinition);
        }
    }


