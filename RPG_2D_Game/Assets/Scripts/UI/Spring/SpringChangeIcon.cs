using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpringChangeIcon : MonoBehaviour
{
    public List<SpringChangeIcon> icons;

    public SpringIconSO iconSO;

    public Button iconButton;
    public Image iconImage;
    public TMP_Text iconText;

    public int currentLevel;
    public int maxLevel;

    public bool isUnlock;

    public static event Action<SpringChangeIcon> OnAbilityPoint;


    private void Start()
    {
        iconButton.onClick.AddListener(() => AddPoints());
    }

    private void OnValidate()
    {
        if(iconSO != null)
           UpdateUI();
    }

    public void UpdateUI()
    {
        iconImage.sprite = iconSO.skillIcon;

        if (isUnlock)
        {
            iconImage.color = Color.white;
            iconText.text = currentLevel + "\\" + maxLevel;
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
        if (isUnlock && currentLevel < maxLevel && DatasManager.Instance.experiencePoints > 0)
        {
            SoundMusics.Instance.PlaySound("switch-a");
            currentLevel++;
            OnAbilityPoint?.Invoke(this);
            UpdateUI();
            if (currentLevel == maxLevel)
            {
                iconButton.interactable = false;
                foreach (SpringChangeIcon nextIcon in icons)
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
