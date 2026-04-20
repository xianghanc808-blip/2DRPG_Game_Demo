using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpringShopSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public TMP_Text slotPrice;
    public Image slotInmage;
    public SpringShopInfoPanel shopInfoPanel;
    public SpringShopSlotsManager shopSlotsManager;

    [Header("not Need Input")]
    public Spring_ItemSO slotItemSO;
    public int price;



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotItemSO != null)
            shopInfoPanel.ShowItemInfo(slotItemSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shopInfoPanel.HideItemInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (slotItemSO != null)
            shopInfoPanel.FollowMouse();
    }

    public void OnBuyButtonClick()
    {
        shopSlotsManager.TryBuyItem(slotItemSO, price);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void UpdateSlot(Spring_ItemSO itemSO, int price)
    {
        slotPrice.text = "$" + price;
        slotInmage.sprite = itemSO.slotIcon;
        slotItemSO = itemSO;
        this.price = price;
    }
}
