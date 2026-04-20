using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringShopSlotsManager : MonoBehaviour
{
    [SerializeField]private SpringShopSlots[] shopSlots;
    [SerializeField]private Inventory_Manager inventoryManager;

    public void PopulateShopItems(List<SpringShopItems> shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            SpringShopItems shopItem = shopItems[i];
            shopSlots[i].UpdateSlot(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }
        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(Spring_ItemSO itemSO, int price)
    {
        if (itemSO != null && DatasManager.Instance.prices >= price)
        {
            if (HasSpaceForItem(itemSO))
            {
                DatasManager.Instance.UpdatePrices(-price);
                inventoryManager.AddItem(itemSO, 1);
            }
        }
    }

    public void SellItem(Spring_ItemSO itemSO)
    {
        if (itemSO == null)
            return;
        foreach (var shopSlot in shopSlots)
        {
            if (shopSlot.slotItemSO == itemSO)
            {
                DatasManager.Instance.UpdatePrices(shopSlot.price - 1);
                return;
            }
        }
    }

    private bool HasSpaceForItem(Spring_ItemSO itemSO)
    {
        foreach (var slot in inventoryManager.slots)
        {
            if(slot.itemSO == itemSO && slot.quantity < inventoryManager.evryStackSize)
            {
                return true;
            }
            else if (slot.itemSO == null)
            {
                return true;
            }
        }
        return false;
    }
}
[System.Serializable]
public class SpringShopItems
{
    public Spring_ItemSO itemSO;
    public int price;
}