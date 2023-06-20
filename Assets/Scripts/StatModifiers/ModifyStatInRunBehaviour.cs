public class ModifyStatInRunBehaviour : IModifyStatBehaviour
{
    public void ExecuteBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {
        characterController.ChangeStatInRun(statModificator);

    }
}
