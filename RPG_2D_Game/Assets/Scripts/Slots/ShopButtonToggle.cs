using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonToggle : MonoBehaviour
{
    public void OnpenItemShop()
    {
        if(ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenItemShop();
        }
    }

    public void OnpenWeaponShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenWeaponShop();
        }
    }

    public void OnpenPotionShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenPotionShop();
        }
    }
}
