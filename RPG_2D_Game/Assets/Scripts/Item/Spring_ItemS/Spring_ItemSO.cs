using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Spring/Item")]
public class Spring_ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite mapIcon;
    public Sprite slotIcon;
    public Items changeitems;

    [Header("addProperties")]
    public int addCurrentHealth;
    public int addSpeed;
    public int addDamage;
    public float duration;

    public List<string> remarkDestroyitems;
}

public enum Items
{
    meat,
    wood,
    gold,
    other,
}
