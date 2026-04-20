using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringShopKeeper : MonoBehaviour
{
    public CanvasGroup shopCanvasGroup;
    private bool isShopOpen ;

    public SpringShopSlotsManager shopSlotsMmanager;

    [SerializeField] private List<SpringShopItems> shopItems;
    [SerializeField] private List<SpringShopItems> shopWeapon;
    [SerializeField] private List<SpringShopItems> shopOption;

    public static event Action<SpringShopSlotsManager, bool> OnShopStateChanged;

    private void Awake()
    {
        OnShopStateChanged?.Invoke(shopSlotsMmanager, true);
        DatasManager.Instance.UpdatePrices(0);
        shopSlotsMmanager.PopulateShopItems(shopItems);
    }

    public void OpenOrCloseShop()
    {
        if (!isShopOpen)
        {
            Time.timeScale = 0;
            isShopOpen = true;
            shopCanvasGroup.alpha = 1;
            shopCanvasGroup.interactable = true;
            shopCanvasGroup.blocksRaycasts = true;
            OpenItemShop();
        }
        else
        {
            Time.timeScale = 1;
            isShopOpen = false;
            shopCanvasGroup.alpha = 0;
            shopCanvasGroup.interactable = false;
            shopCanvasGroup.blocksRaycasts = false;

            
        }
    }


    public void OpenItemShop()
    {
        shopSlotsMmanager.PopulateShopItems(shopItems);
        SoundMusics.Instance.PlaySound("switch-a");
    }
    public void OpenWeaponShop()
    {
        shopSlotsMmanager.PopulateShopItems(shopWeapon);
        SoundMusics.Instance.PlaySound("switch-a");
    }
    public void OpenPtionShop()
    {
        shopSlotsMmanager.PopulateShopItems(shopOption);
        SoundMusics.Instance.PlaySound("switch-a");
    }
}
