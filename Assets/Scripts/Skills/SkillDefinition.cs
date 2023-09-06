using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Definition", menuName = "Skills/Skill Definition")]
public class SkillDefinition : ScriptableObject
{
    public new PlayerEnumSkills name;
    [TextArea] public new string description;
    public List<InstantEffectTemporallyDefinition> temporalInstantEffects = new List<InstantEffectTemporallyDefinition>();
    public List<InstantEffectPermanentDefinition> permanentInstantEffects = new List<InstantEffectPermanentDefinition>();
    public List<OverTimeEffect> overTimeEffects = new List<OverTimeEffect>();
    public List<DuringTimeEffect> duringTimeEffects = new List<DuringTimeEffect>();

    public async Task ProcessSkill(Characters.CharacterController owner, Characters.CharacterController target)
    {
        ProcessTemporalInstantEffects(owner, target);
        ProcessDuringTimeEffects(owner, target);
        ProcessOverTimeEffects(owner, target);
    }

    public async Task ProcessSkill(Characters.CharacterController owner)
    {
        ProcessPermanentInstantEffects(owner);
    }

    private void ProcessTemporalInstantEffects(Characters.CharacterController owner, Characters.CharacterController target)
    {
        foreach(InstantEffectTemporallyDefinition effect in temporalInstantEffects)
        {
            effect.ProcessEffect(owner, target);
        }
    }
    /**
     * TODO
     * Move from here to another kind of skill definition.
     */
    private void ProcessPermanentInstantEffects(Characters.CharacterController owner)
    {
        foreach(InstantEffectPermanentDefinition effect in permanentInstantEffects)
        {
            effect.ProcessEffect(owner);
        }
    }
    private void ProcessOverTimeEffects(Characters.CharacterController owner, Characters.CharacterController target)
    {
        foreach (OverTimeEffect effect in overTimeEffects)
        {
            effect.ProcessEffect(owner, target);
        }
    }

    private void ProcessDuringTimeEffects(Characters.CharacterController owner, Characters.CharacterController target)
    {
        foreach (DuringTimeEffect effect in duringTimeEffects)
        {
            effect.ProcessEffect(owner, target);
        }
    }
}
