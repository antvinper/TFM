public class ModifyStatePermanentBehaviour : IModifyStateBehaviour
{
    public void ExecuteBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {
        characterController.ChangeStatPermanent(statModificator);
    }
}
