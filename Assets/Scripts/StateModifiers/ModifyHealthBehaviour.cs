public class ModifyHealthBehaviour : IModifyStateBehaviour
{
    public void ExecuteBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {
        if (statModificator.IsPercentual)
        {
            characterController.ChangePercentualHealth(statModificator);
        }
        else
        {
            characterController.ChangeRealHealth(statModificator);
        }
    }
}

