using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public ItemSo itemSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    private ItemUIManager inventoryManager;
    private static ShopSlotsManager activeShop;

    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }
    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }

    private void HandleShopStateChanged(ShopSlotsManager shopSlotsManager, bool isOpen)
    {
        activeShop = isOpen ? shopSlotsManager : null;
    }


    private void Start()
    {
        inventoryManager = GetComponentInParent<ItemUIManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if (activeShop != null)
                {
                    activeShop.SellItem(itemSO);
                    quantity--;
                    UpdateUI();
                }
                else
                {
                    if (itemSO.currentHealth > 0 && StatsManager.Instance.currentHealth >= StatsManager.Instance.maxHealth)
                        return;
                    inventoryManager.UseItem(this);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
    }

    private void OnValidate()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (quantity <= 0)
            itemSO = null;

        if(itemSO != null)
        {
            itemImage.sprite = itemSO.slotIcon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }

    
}

