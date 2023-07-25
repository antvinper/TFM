using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree Slot Definition", menuName = "Tree/Slot Definition")]
public class TreeSlotDefinition : ScriptableObject
{
    [SerializeField] private List<InstantEffectPermanent> effects = new List<InstantEffectPermanent>();
    [SerializeField] private bool isActive;
    public List<InstantEffectPermanent> Effects
    {
        get { return effects; }
    }

    public async Task ProcessSlotActivation(Characters.CharacterController target)
    {
        foreach(InstantEffectPermanent effect in effects)
        {
            effect.ProcessEffect(target);
        }
        isActive = true;
    }

    public async Task ProcessSlotDeActivation()
    {
        foreach (InstantEffectPermanent effect in effects)
        {
            effect.RemoveEffect();
        }
        isActive = false;
    }
}
