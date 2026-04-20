using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconSlots : MonoBehaviour
{

    public List<IconSlots> prerequisiteSkillSlots;
    public Image iconImage;
    public TMP_Text levelText;

    public IconSO iconSO;

    public Button skillButton;

 
    public static event Action OnSkillMaxed;

    public bool isUnlocked;
    public int currentLevel;

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        iconImage.sprite = iconSO.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;
            levelText.text = currentLevel.ToString() + "/" + iconSO.maxLevel.ToString();
            iconImage.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            levelText.text = "";
            iconImage.color = Color.grey;
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    public bool CanUnlockSkill()
    {
        foreach (IconSlots slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || currentLevel < iconSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < iconSO.maxLevel)
        {
            currentLevel++;
            if (currentLevel >= iconSO.maxLevel)
            {
                OnSkillMaxed?.Invoke();
            }
            UpdateUI();
        }
    }
}
