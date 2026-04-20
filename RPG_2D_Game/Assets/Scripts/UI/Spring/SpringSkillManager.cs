using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringSkillManager : MonoBehaviour
{
    private void OnEnable()
    {
        SpringChangeIcon.OnAbilityPoint += HandleAbilityPoints;
    }
    private void OnDisable()
    {
        SpringChangeIcon.OnAbilityPoint -= HandleAbilityPoints;
    }

    private void HandleAbilityPoints(SpringChangeIcon icon)
    {

        if (DatasManager.Instance.experiencePoints < 0)
            return;

        if (DatasManager.Instance.experiencePoints > 0)
        {
            DatasManager.Instance.UpdateAbilityPoints(-1);
        }
        string skillName = icon.iconSO.skillName;

        switch (skillName)
        {
            case "Health":
                DatasManager.Instance.AddMaxHealth(1);
                DatasManager.Instance.UpdateCurrentHealth(1);
                break;
            case "Attack":
                DatasManager.Instance.UpdateDamage(1);
                break;
            case "Speed":
                DatasManager.Instance.UpdateSpeed(1);
                break;
            default:
                Debug.LogWarning("UnKnow Skill:" + skillName);
                break;
        }
    }

}
