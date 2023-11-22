using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Tree Slot Definition", menuName = "Tree/Slot Definition")]
public class TreeSlotDefinition : ScriptableObject
{
    [SerializeField] private List<InstantEffectPermanent> effects = new List<InstantEffectPermanent>();
    [SerializeField] private bool isActive;
    [JsonProperty]
    public List<InstantEffectPermanent> Effects
    {
        get => effects;
        set => effects = value;
    }
    [JsonProperty]
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public async Task ProcessSlotActivation(CompanyCharacterController target)
    {
        foreach(InstantEffectPermanent effect in effects)
        {
            effect.ProcessEffect(target);
        }
        isActive = true;
    }

    public async Task ProcessSlotDeActivation(CompanyCharacterController target)
    {
        foreach (InstantEffectPermanent effect in effects)
        {
            effect.RemoveEffect(target);
        }
        isActive = false;
    }
}
