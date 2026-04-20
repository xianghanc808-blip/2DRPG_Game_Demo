using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.2f;

    public Slider expSlider;
    public TMP_Text currentLevelText;
    public TMP_Text levelText;

    private void OnEnable()
    {
        //Enemy01.OnMonsterDefeated += GainExperiance;
        ItemUIManager.OnExperienceGained += GainExperiance;
    }
    private void OnDisable()
    {
        //Enemy01.OnMonsterDefeated -= GainExperiance;
        ItemUIManager.OnExperienceGained -= GainExperiance;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.T))
        {
            GainExperiance(5);
        }
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Experience:" + currentExp;
        levelText.text = "Level:" + level;
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);

    }
    public void GainExperiance(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }
}
