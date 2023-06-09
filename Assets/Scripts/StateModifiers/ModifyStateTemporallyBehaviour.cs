public class ModifyStateTemporallyBehaviour : IModifyStateBehaviour
{
    public void ExecuteBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {
        characterController.ChangeStatTemporally(statModificator);

    }
}
