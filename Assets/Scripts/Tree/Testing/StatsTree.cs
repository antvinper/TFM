using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using CompanyStats;

[System.Serializable]
[CreateAssetMenu(fileName = "Tree Definition", menuName = "Tree/Tree Definition")]
public class StatsTree : ScriptableObject
{
    /**
     * TODO
     * VER DOCUMENTO Enemigos y Personaje
     * Para ver cómo está diseñado e implementar lo que sea
     * necesario para que vaya-
     */

    [SerializeField] private List<TreeSlotDefinition> slots;
    [JsonProperty]
    public List<TreeSlotDefinition> Slots
    {
        get => slots;
        set => slots = value;
    }

    public async Task ProcessSlotActivation(TreeSlotDefinition slot)
    {
        int index = slots.FindIndex(s => s.Equals(slot));

        /*foreach (InstantEffectPermanent effect in slot.Effects)
        {
            effect.ProcessEffect(target, true, index);
        }*/

        slots[index].IsActive = true;
    }

    public async Task ProcessSlotDeActivation(TreeSlotDefinition slot)
    {
        int index = slots.FindIndex(s => s.Equals(slot));
        /*foreach (InstantEffectPermanent effect in slot.Effects)
        {
            effect.RemoveEffect(target, true, index);
        }*/
        slots[index].IsActive = false;
    }
}
