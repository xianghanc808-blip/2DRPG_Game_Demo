using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    public static Inventory_Manager Instance;

    public Slot_Manager[] slots;
    public int evryStackSize;
    public GameObject itemPrefab;
    public Transform dropPosition;
    public static bool isCanFilling;
    public Spring_UseSlots useSlots;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SpringItem.OnItemLooted += AddItem;
        SpringItem.OnCanFilling += FindSpace;
    }

    private void OnDisable()
    {
        SpringItem.OnItemLooted -= AddItem;
        SpringItem.OnCanFilling -= FindSpace;
    }

    
    public void UseItem(Slot_Manager slot)
    {
        if(slot.itemSO != null && slot.quantity > 0)
        {
            useSlots.ApplyItemEffects(slot.itemSO);
            slot.quantity -= 1;
            if(slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }


    public void AddItem(Spring_ItemSO item, int quantity)
    {

        SoundMusics.Instance.PlaySound("PickUpItemSound");
        if(item.changeitems == Items.gold)
        {
            DatasManager.Instance.UpdatePrices(quantity);
            return;
        }


        DatasManager.Instance.hasItemNow.Add(item);
      

        foreach (var slot in slots)
        {
            if(slot.itemSO == item && slot.quantity < evryStackSize)
            {

                int availableSpace = evryStackSize - quantity;
                int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUI();
                if (quantity <= 0)
                    return;
            }
        }

        foreach(var slot in slots)
        {
            if(slot.itemSO == null)
            {

                int amountToAdd = Mathf.Min(quantity, evryStackSize);
                slot.itemSO = item;
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;
                slot.UpdateUI();
                if (quantity <= 0)
                    return;
            }
        }

        if (quantity > 0)
        {
            isCanFilling = false;
            DropLoot(item, quantity);
        }
    }

    public void RemoveItem(Spring_ItemSO itemSO, int quantity)
    {

        DatasManager.Instance.hasItemNow.Remove(itemSO);
        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];

            if (slot.itemSO != itemSO)
                continue;

            if (slot.quantity > quantity)
            {
                slot.quantity -= quantity;
                slot.UpdateUI();
                quantity = 0;
                     
            }
            else
            {
                quantity -= slot.quantity;
                slot.itemSO = null;
                slot.quantity = 0;
                slot.UpdateUI();
            }
        }
    }

    public  bool FindSpace(Spring_ItemSO item)
    {
        foreach(var slot in slots)
        {
            if(slot.quantity < evryStackSize && slot.itemSO == item || slot.itemSO == null)
            {

                return true;
            }
        }
        return false;
    }

    public void DropItem(Slot_Manager slot)
    {
        DropLoot(slot.itemSO, 1);

        slot.quantity -=1;

        if(slot.quantity <= 0)
        {
            slot.itemSO = null;
        }
        slot.UpdateUI();
    }

     private void DropLoot(Spring_ItemSO itemSO, int quantity)
    {
        itemPrefab.GetComponentInChildren<SpriteRenderer>().sprite = itemSO.mapIcon;
        SpringItem item = Instantiate(itemPrefab, dropPosition.position, Quaternion.identity).GetComponent<SpringItem>();
        item.Initialize(itemSO, quantity);
     }

    public int GetItemQuantity(Spring_ItemSO itemSO)
    {
        int total = 0;

        foreach (var slot in slots)
        {
            if (slot.itemSO == itemSO)
                total += slot.quantity;
        }

        return total;
    }

    public bool HasItems(Spring_ItemSO itemSO) 
    {
        foreach (var slot in slots)
        {
            if(slot.itemSO == itemSO && slot.quantity > 0)
            {
                return true;
            }
        }
        return false;
    }
}
