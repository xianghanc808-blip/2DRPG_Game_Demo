using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Warrior_Experience : MonoBehaviour
{
    

    public Slider expSlider;
    public TMP_Text currentLevelText;
    public TMP_Text currentExpText;

    private int level;
    private int expToLevel = 10;
    private float expGrowthMultiplier = 1.2f;
    private int currentExp;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        Torch_Health.OnMonsterDefeated += GainExperiance;
    }
    private void OnDisable()
    {
        Torch_Health.OnMonsterDefeated -= GainExperiance;
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

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);

    }

    public void UpdateUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        
        currentLevelText.text = level.ToString();
        currentExpText.text = currentExp + "\\" + expToLevel;
    }
}
