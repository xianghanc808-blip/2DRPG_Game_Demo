using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSo itemSo;

    public SpriteRenderer sr;
    public Animator anim;

    private bool canPickup = true;

    public int quantity;
    public static event Action<ItemSo, int> OnItemLooted;

    private void OnValidate()
    {
        if (itemSo == null)
            return;
        UpdateAppearance();
    }

    public void Initialize(ItemSo itemSo, int quantity)
    {
        this.itemSo = itemSo;
        this.quantity = quantity;
        canPickup = false;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        sr.sprite = itemSo.icon;
        this.name = itemSo.itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canPickup)
        {
            anim.Play("Pickup");
            OnItemLooted?.Invoke(itemSo, quantity);
            Destroy(this.gameObject, 1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canPickup == false)
        {
            canPickup = true;
        }
    }
}
