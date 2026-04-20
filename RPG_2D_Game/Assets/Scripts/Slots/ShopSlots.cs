using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public TMP_Text slotPrice;
    public Image slotImage;
    public ItemSo slotItemSo;
    [SerializeField] private ShopSlotsManager shopSlotsManager;
    [SerializeField] private InfoPanel shopInfo;

    public int price;
    public void Initialized(ItemSo itemSO, int price)
    {
        slotPrice.text = price.ToString();
        slotImage.sprite = itemSO.slotIcon;
        slotItemSo = itemSO;
        this.price = price;
    }

    public void OnBuyButtonClicked()
    {
        shopSlotsManager.TryBuyItem(slotItemSo, price);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotItemSo != null)
            shopInfo.ShowItemInfo(slotItemSo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shopInfo.HideItemInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (slotItemSo != null)
            shopInfo.FollowMouse();
    }
}
