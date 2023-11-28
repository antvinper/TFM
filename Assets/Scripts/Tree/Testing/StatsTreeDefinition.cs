using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using CompanyStats;

[System.Serializable]
[CreateAssetMenu(fileName = "Tree Definition", menuName = "Tree/Tree Definition")]
public class StatsTreeDefinition : ScriptableObject
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

    /*public async Task ProcessSlotActivation(TreeSlotDefinition slot)
    {
        int index = slots.FindIndex(s => s.Equals(slot));
        slots[index].ProcessSlotActivation();
    }*/

    /*public async Task ProcessSlotDeActivation(TreeSlotDefinition slot)
    {
        int index = slots.FindIndex(s => s.Equals(slot));
        
        slots[index].IsActive = false;
    }*/
}
