using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree Slot Definition", menuName = "Tree/Slot Definition")]
public class TestTreeSlotDefinition : ScriptableObject
{
    [SerializeField] private TestTreeSlotDefinition previousSlot;
    [SerializeField] private List<InstantEffectPermanent> effects = new List<InstantEffectPermanent>();

    public async Task ProcessSlotActivation(Characters.CharacterController target)
    {
        foreach(InstantEffectPermanent effect in effects)
        {
            effect.ProcessEffect(target);
        }
    }
}
