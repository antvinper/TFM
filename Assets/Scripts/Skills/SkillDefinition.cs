using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Definition", menuName = "Skills/Skill Definition Definition")]
public class SkillDefinition : ScriptableObject
{
    public new PlayerEnumSkillsTest name;
    [TextArea] public new string description;
    public List<InstantEffectDefinition> instantEffects = new List<InstantEffectDefinition>();
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
        foreach(InstantEffectDefinition effect in instantEffects)
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
