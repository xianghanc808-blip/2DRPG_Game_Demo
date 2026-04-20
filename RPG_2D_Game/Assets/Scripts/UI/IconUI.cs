using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    public IconSlots[] iconSlots;
    private void OnEnable()
    {
        
        IconSlots.OnSkillMaxed += HandleSkillMaxed;
    }
    private void OnDisable()
    {
     
        IconSlots.OnSkillMaxed -= HandleSkillMaxed;
    }

    private void HandleSkillMaxed()
    {
        foreach (IconSlots slot in iconSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }

    private void Start()
    {
        foreach (IconSlots slot in iconSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckavailablePoints(slot));
        }
    }

    private void CheckavailablePoints(IconSlots slot)
    {
        
            slot.TryUpgradeSkill();
        
    }

}
