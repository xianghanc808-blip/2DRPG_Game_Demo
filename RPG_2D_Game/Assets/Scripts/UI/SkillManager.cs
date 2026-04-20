using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int experiencePoints;
    public TMP_Text textPoints;

    public CanvasGroup statsCanvas;
    private bool skillTreeOpen;

    private void OnEnable()
    {
        ChangeIcon.OnAbilityPoint += HandleAbilityPoints;
    }
    private void OnDisable()
    {
        ChangeIcon.OnAbilityPoint -= HandleAbilityPoints;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }
    }

    private void HandleAbilityPoints(ChangeIcon icon)
    {
        if(experiencePoints > 0)
        {
            DatasManager.Instance.UpdateAbilityPoints(-1);
        }

        string skillName = icon.iconSO.skillName;
        switch (skillName)
        {
            case "Attack":
                StatsManager.Instance.damage += 1;
                break;
            case "Guard":
                StatsManager.Instance.guard -= 0.1f;
                break;
            case "Health":
                StatsManager.Instance.UpdateMaxHealth(1);
                break;
            default:
                Debug.LogWarning("Unknow skill:" + skillName);
                break;

        }
    }

   
}
