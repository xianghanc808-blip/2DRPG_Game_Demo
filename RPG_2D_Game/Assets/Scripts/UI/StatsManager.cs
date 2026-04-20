using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    public TMP_Text healthText;
    public Slider playerhealthSlider;
    public StatsUI statsUI;

    [Header("Player Health Stats")]
    public float maxHealth;
    public float currentHealth;

    [Header("player Health Stats")]
    public float speed;

    [Header("Coombat Stats")]
    public int damage;
    public float attackRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    public float guard = 1f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP:" + currentHealth + "\\" + maxHealth;
        statsUI.UpdateHealth();
    }
    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateSpeed();
    }
    public void UpdateDamage(int amount)
    {
        damage += amount;
        statsUI.UpdateDmage();
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

        playerhealthSlider.value =  currentHealth / maxHealth;

        healthText.text = "HP:" + currentHealth + "\\" + maxHealth;
    }
}
