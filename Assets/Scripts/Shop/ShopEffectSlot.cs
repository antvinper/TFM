
public class ShopEffectSlot : ShopSlot
{
    private int index;
    EffectItem effectItem;
    public void Setup(EffectItem effectItem, int index)
    {
        this.effectItem = effectItem;
        string name = effectItem.GetEffectName();
        string description = effectItem.GetEffectDescription();
        int value = effectItem.GetEffectValueInPercentage();
        name += ": " + value + "%";
        this.index = index;
        base.Setup(name, description, effectItem.Price, effectItem.Sprite);
    }

    public void Purchase()
    {
        base.Purchase();
        this.effectItem.UseItem(GameManager.Instance.GetPlayerController());
        GetComponentInParent<Shop>().DeActivateSlot(index);
    }
}
