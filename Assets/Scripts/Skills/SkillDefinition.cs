using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTest", menuName = "Effects/Skill Definition Test")]
public class SkillDefinition : ScriptableObject
{
    public new PlayerEnumSkillsTest name;
    [TextArea] public new string description;
    public List<InstantEffect> instantEffects = new List<InstantEffect>();
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
        foreach(InstantEffect effect in instantEffects)
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
