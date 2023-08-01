
public class ShopEffectSlot : ShopSlot
{
    private EffectItem effectItem;
    public void Setup(EffectItem effectItem, int index)
    {
        this.effectItem = effectItem;

        //string name = effectItem.GetEffectName();
        //string description = effectItem.GetEffectDescription();

        /*if (effectItem.IsValueInPercentage())
        {
            int value = effectItem.GetEffectValueInPercentage();
            effectItem.NameSufix = ": " + value + "%";
        }
        else
        {
            int value = effectItem.GetEffectValue();
            effectItem.NameSufix += ": " + value;
        }*/

        this.index = index;
        base.Setup(effectItem);
    }

    public void Purchase()
    {
        base.Purchase();
        this.effectItem.UseItem(ShopManager.Instance.Shop.PlayerController);
        GetComponentInParent<Shop>().DeActivateSlot(index);
    }
}
