using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatasManager : MonoBehaviour
{
    public static DatasManager Instance;

    public UIStats uiStats;

    public TMP_Text healthText;

    public TMP_Text textPoints;

    public int prices;
    public TMP_Text textPrices;
    public TMP_Text textPlayerPrices;
    public HashSet<Spring_ItemSO> hasItemNow = new();
    public static Action<int> OnHealthChange;
    public static Action<int> OnAddMashealth;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Player Property")]
    public int maxHealth;
    public int currentHealth;
    public int speed;
    public int damamge;
    public int experiencePoints;

    public void UpdateCurrentHealth(int amount)
    {
        AddHealth(amount);
        uiStats.UpdateHealth();
    }

    public void UpdateDamage(int amount)
    {
        damamge += amount;
        uiStats.UpdateDamage();
    }

    public void UpdateSpeed(int speed)
    {
        this.speed += speed;
        uiStats.UpdateSpeed();
    }

    public void AddHealth(int amount)
    {
        
        currentHealth += amount;
        
        OnHealthChange?.Invoke(currentHealth);

        healthText.text = currentHealth + "\\" + maxHealth;
    }

    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        
        OnHealthChange?.Invoke(maxHealth);

        healthText.text = currentHealth + "\\" + maxHealth;
    }

    public void UpdateAbilityPoints(int amount)
    {
        experiencePoints += amount;
        textPoints.text = experiencePoints.ToString();
    }

    public void UpdatePrices(int amount)
    {
        prices += amount;
        textPlayerPrices.text = prices.ToString();
    }
}
