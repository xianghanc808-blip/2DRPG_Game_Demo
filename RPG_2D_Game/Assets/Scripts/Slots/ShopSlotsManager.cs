using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlotsManager : MonoBehaviour
{
    [SerializeField] private ShopSlots[] shopSlots;
    [SerializeField] private ItemUIManager itemUIManager;
    public void PopulateShopItems(List<ShopItems> shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialized(shopItem.itemSo, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }
        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemSo itemSO, int price)
    {
        if (itemSO != null && itemUIManager.amount >= price)
        {
            if (HasSpaceForItem(itemSO))
            {
                itemUIManager.amount -= price;
                itemUIManager.goldText.text = itemUIManager.amount.ToString();
                itemUIManager.AddItem(itemSO, 1);
            }
        }
    }

    private bool HasSpaceForItem(ItemSo itemSo)
    {
        foreach (Slot slot in itemUIManager.slots)
        {
            if(slot.itemSO == itemSo && slot.quantity < itemSo.stackSize)
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

    public void SellItem(ItemSo itemSo)
    {
        if (itemSo == null)
            return;
        foreach (ShopSlots shopSlot in shopSlots)
        {
            if (shopSlot.slotItemSo == itemSo)
            {
                itemUIManager.amount += shopSlot.price -1;
                itemUIManager.goldText.text = itemUIManager.amount.ToString();
                return;
            }
        }
    }
}

[System.Serializable]
public class ShopItems
{
    public ItemSo itemSo;
    public int price;
}
