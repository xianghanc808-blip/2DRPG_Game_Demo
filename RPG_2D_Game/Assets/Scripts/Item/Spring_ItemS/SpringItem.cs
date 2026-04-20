using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringItem : MonoBehaviour
{
    public Spring_ItemSO itemSO;
    public SpriteRenderer sr;
    public int quantity;
    public Animator anim;


    public static event Action<Spring_ItemSO, int> OnItemLooted;
    public static event Func<Spring_ItemSO, bool> OnCanFilling;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnCanFilling(itemSO))
        {
            if (collision.CompareTag("Player"))
            {
                anim.Play("NormalPickup");
                OnItemLooted?.Invoke(itemSO, quantity);
                Destroy(this.gameObject, 1f);
                itemSO.remarkDestroyitems.Add(itemSO.name);
            }
        }
    }
    public void Initialize(Spring_ItemSO otheritemSO, int quantity)
    {
        
        itemSO = otheritemSO;
        this.quantity = quantity;
        this.name = this.itemSO.name;
        switch (itemSO.changeitems)
        {
            case Items.meat:
                anim.Play("MeatDiscard");
                break;
            case Items.wood:
                anim.Play("WoodDiscard");
                break;
            default:
                break;
        }
        
    }

    private void OnValidate()
    {
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        sr.sprite = this.itemSO.mapIcon;
        this.name = this.itemSO.name;
    }
}
