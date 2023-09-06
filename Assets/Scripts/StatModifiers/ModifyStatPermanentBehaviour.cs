public class ModifyStatPermanentBehaviour : IModifyStatBehaviour
{
    public void ExecuteBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {
        characterController.ChangeStatPermanent(statModificator);
    }
}
