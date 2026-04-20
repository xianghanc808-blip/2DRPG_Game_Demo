using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    public List<ChangeIcon> icons;
    public IconSO iconSO;
    public Image iconImage;
    public TMP_Text iconText;
    public int currentLevel;
    public int maxLevel;
    public Button iconButton;
    public bool isUnlock;
    public static event Action<ChangeIcon> OnAbilityPoint;

    private void Start()
    {
        UpdateUI();
        iconButton.onClick.AddListener(() => AddPoints());
    }

    public void UpdateUI()
    {
        iconImage.sprite = iconSO.skillIcon;

        if (isUnlock)
        {
            iconImage.color = Color.white;
            iconText.text = currentLevel + "/" + maxLevel;
            iconButton.interactable = true;
        }
        else
        {
            iconImage.color = Color.gray;
            iconText.text = "";
            iconButton.interactable = false;
        }
    }
    public void AddPoints()
    {
        if (isUnlock && currentLevel < maxLevel)
        {
            currentLevel++;
            OnAbilityPoint?.Invoke(this);
            UpdateUI();
            Debug.Log(currentLevel);
            if (currentLevel == maxLevel)
            {
                foreach (ChangeIcon nextIcon in icons)
                {
                    if (nextIcon != null)
                    {
                        nextIcon.isUnlock = true;
                        nextIcon.UpdateUI();
                    }
                }
            }
        }
       
    }

}
