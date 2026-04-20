using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Manager : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public TMP_Text quantityText;
    public SpringShopInfoPanel springShopInfoPanel;
    public Button buttonUseItem;
    public Button buttonSellItems;


    [Header("Not need set")]
    public int quantity;
    public Spring_ItemSO itemSO;
    private Inventory_Manager inventoryManager;
    private static SpringShopSlotsManager activeShop;
    private int sellItemQuantity;

    private void Start()
    {
        inventoryManager = GetComponentInParent<Inventory_Manager>();
        
    }

    private void OnEnable()
    {
        SpringShopKeeper.OnShopStateChanged += HandleShopStateChange;
        
    }
    private void OnDisable()
    {
        SpringShopKeeper.OnShopStateChanged -= HandleShopStateChange;
    }

    private void HandleShopStateChange(SpringShopSlotsManager shopSlotsManager, bool isOpen)
    {
        activeShop = isOpen ? shopSlotsManager : null;
    }


    private void Update()
    {
        
        if (DatasManager.Instance.currentHealth >= DatasManager.Instance.maxHealth)
        {
            buttonUseItem.interactable = false;
        }
        else
        {
            buttonUseItem.interactable = true;
        }

    }


    public void UpdateUI()
    {
        if (quantity <= 0)
        {
            itemSO = null;
            buttonSellItems.interactable = false;
        }
        else
        {
            buttonSellItems.interactable = true;
        }

        if (itemSO != null)
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


    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if(sellItemQuantity == 1)
            {
                buttonSellItems.onClick.RemoveAllListeners();
                buttonUseItem.onClick.RemoveAllListeners();
                sellItemQuantity = 0;
            }

            UpdateUI();
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SoundMusics.Instance.PlaySound("switch-a");
                springShopInfoPanel.ShowItemInfo(itemSO);
                springShopInfoPanel.infoPanel.alpha = 1; 
                springShopInfoPanel.infoPanel.interactable = true;
                springShopInfoPanel.infoPanel.blocksRaycasts = true;

                if(sellItemQuantity != 1)
                {
                    buttonSellItems.onClick.AddListener(OnSellItems);
                    buttonUseItem.onClick.AddListener(OnUseItems);
                    sellItemQuantity = 1;
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                SoundMusics.Instance.PlaySound("switch-a");
                inventoryManager.DropItem(this);
            }
        }
    }

    public void OnUseItems( )
    {
        inventoryManager.UseItem(this);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void OnSellItems()
    {
        //activeShop.SellItem(itemSO);
        SoundMusics.Instance.PlaySound("switch-a");
        DatasManager.Instance.UpdatePrices(1);
        quantity -= 1;
        UpdateUI();
    }

    public void OnCloseItemsPanel()
    {
        SoundMusics.Instance.PlaySound("switch-a");
        springShopInfoPanel.infoPanel.alpha = 0;
        springShopInfoPanel.infoPanel.interactable = false;
        springShopInfoPanel.infoPanel.blocksRaycasts = false;
    }
}
