using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public CanvasGroup skillCanvas;

    private bool skillClose;

    public void CloseOrOpenStats()
    {
        if (skillClose)
        {
            skillClose = false;
            Time.timeScale = 1;
            skillCanvas.alpha = 0;
            skillCanvas.blocksRaycasts = false;
            DatasManager.Instance.UpdateAbilityPoints(1);
            SoundMusics.Instance.PlaySound("switch-a");
        }
        else
        {
            skillClose = true;
            Time.timeScale = 0;
            skillCanvas.alpha = 1;
            skillCanvas.blocksRaycasts = true;
            SoundMusics.Instance.PlaySound("switch-a");
        }
    }
}
