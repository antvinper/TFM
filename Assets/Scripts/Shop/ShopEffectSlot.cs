
public class ShopEffectSlot : ShopSlot
{
    private EffectItem effectItem;
    public void Setup(EffectItem effectItem, int index, ShopController shopController)
    {
        this.effectItem = effectItem;

        this.index = index;
        base.Setup(effectItem, shopController);
    }

    public void Purchase()
    {
        base.Purchase();
        shopController.ApplyEffectPurchase(price, effectItem, index);
    }
}
