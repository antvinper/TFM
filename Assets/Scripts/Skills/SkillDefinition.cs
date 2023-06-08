using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Definition", menuName = "Skills/Skill Definition")]
public class SkillDefinition : ScriptableObject
{
    public new PlayerEnumSkills name;
    [TextArea] public new string description;
    public List<InstantEffectDefinitionForRun> instantEffects = new List<InstantEffectDefinitionForRun>();
    public List<OverTimeEffect> overTimeEffects = new List<OverTimeEffect>();
    public List<DuringTimeEffect> duringTimeEffects = new List<DuringTimeEffect>();

    public async Task ProcessSkill(Characters.CharacterController owner, Characters.CharacterController target)
    {
        ProcessInstantEffects(owner, target);
        ProcessDuringTimeEffects(owner, target);
        ProcessOverTimeEffects(owner, target);
    }

    private void ProcessInstantEffects(Characters.CharacterController owner, Characters.CharacterController target)
    {
        foreach(InstantEffectDefinitionForRun effect in instantEffects)
        {
            effect.ProcessEffect(owner, target);
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
