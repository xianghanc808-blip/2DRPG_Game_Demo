using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;

    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopSlotsManager shopSlotsManager;

    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopPotion;

    [SerializeField] private Camera shopkeeperCamera;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -1);

    public static event Action<ShopSlotsManager, bool> OnShopStateChanged;

    private bool playerInRange;
    private bool isShopOpen;


    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    isShopOpen = true;
                    OnShopStateChanged?.Invoke(shopSlotsManager, true);
                    currentShopKeeper = this;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;

                    shopkeeperCamera.transform.position = transform.position + cameraOffset;
                    shopkeeperCamera.gameObject.SetActive(true);

                    OpenItemShop();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                isShopOpen = false;
                OnShopStateChanged?.Invoke(shopSlotsManager, false);
                currentShopKeeper = null;
                shopCanvasGroup.alpha = 0;
                shopCanvasGroup.blocksRaycasts = false;
                shopCanvasGroup.interactable = false;

                shopkeeperCamera.gameObject.SetActive(false);
            }
            
        }
    }

    public void OpenItemShop()
    {
        shopSlotsManager.PopulateShopItems(shopItems);
    }

    public void OpenWeaponShop()
    {
        shopSlotsManager.PopulateShopItems(shopWeapons);
    }

    public void OpenPotionShop()
    {
        shopSlotsManager.PopulateShopItems(shopPotion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Tipe", true);
            playerInRange = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Tipe", false);
            playerInRange = false;
        }
    }
}
