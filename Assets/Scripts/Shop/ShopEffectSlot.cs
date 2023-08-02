
public class ShopEffectSlot : ShopSlot
{
    private EffectItem effectItem;
    public void Setup(EffectItem effectItem, int index, Shop shop)
    {
        this.effectItem = effectItem;

        this.index = index;
        base.Setup(effectItem, shop);
    }

    public void Purchase()
    {
        base.Purchase();
        this.effectItem.UseItem(ShopManager.Instance.Shop.PlayerController);
        GetComponentInParent<Shop>().DeActivateSlot(index);
    }
}
