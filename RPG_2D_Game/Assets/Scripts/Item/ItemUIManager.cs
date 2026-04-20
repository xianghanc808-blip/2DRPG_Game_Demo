using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class ItemUIManager : MonoBehaviour
{
    public static ItemUIManager Instance;
    public Slot[] slots;
    public TMP_Text goldText;
    public int amount;

    public UseSlots useSlots;

    public GameObject itemPrefab;
    public Transform dropPosition;


    public static event Action<int> OnExperienceGained;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        goldText.text = amount.ToString();
        foreach (Slot slot in slots)
        {
            slot.UpdateUI();
        }
    }

    private void OnEnable()
    {
        Item.OnItemLooted += AddItem;
    }
    private void OnDisable()
    {
        Item.OnItemLooted -= AddItem;
    }

    public void UseItem(Slot slot)
    {
        if (slot.itemSO != null && slot.quantity > 0)
        {
            useSlots.ApplyItemEffects(slot.itemSO);
            slot.quantity--;
            if(slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }

    public void AddItem(ItemSo item, int quantity)
    {
        if (item.isGold)
        {
            amount += quantity;
            goldText.text = amount.ToString();
            return;
        }

        if (item.isEXP)
        {
            OnExperienceGained?.Invoke(quantity);
            return;
        }

        foreach (var slot in slots)
        {
            if (slot.itemSO == item && slot.quantity < item.stackSize)
            {
                int availableSapce = item.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(availableSapce, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;
                
                slot.UpdateUI();
                if (quantity <= 0)
                    return;
            }
        }

        

        foreach (Slot slot in slots)
        {
            if (slot.itemSO == null)
            {
                int amountToAdd = Mathf.Min(item.stackSize, quantity);
                slot.itemSO = item;
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;
                slot.UpdateUI();
                return;
            }
        }


        if (quantity > 0)
        {
            DropLoot(item, quantity);
        }
    }

    public void RemoveItem(ItemSo itemSO, int quantity)
    {
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

    public void DropItem(Slot slot)
    {
        DropLoot(slot.itemSO, 1);
        slot.quantity--;
        if(slot.quantity <= 0)
        {
            slot.itemSO = null;
        }
        slot.UpdateUI();
    }

    private void DropLoot(ItemSo itemSo, int quantity)
    {
        Item item = Instantiate(itemPrefab, dropPosition.position, Quaternion.identity).GetComponent<Item>();
        item.Initialize(itemSo, quantity);
    }

    public bool HasItem(ItemSo itemSo)
    {
        foreach (Slot slot in slots)
        {
            if(slot.itemSO == itemSo && slot.quantity > 0)
            {
                return true;
            }
        }
        return false;
    }

    public int GetItemQuantity(ItemSo itemSo)
    {
        int toltal = 0;

        foreach(var slot in slots)
        {
            if (slot.itemSO == itemSo)
                toltal += slot.quantity;
        }

        return toltal;
    }
}
