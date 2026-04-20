using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public TMP_Text[] statsTexts;
    public CanvasGroup statsCanvas;
    private bool statsClose;

    private void Start()
    {
        UpdateAll();
    }

    public void UpdateDamage()
    {
        statsTexts[0].text = "攻击力:+" + DatasManager.Instance.damamge.ToString();
    }

    public void UpdateSpeed()
    {
        statsTexts[1].text = "速度:+" + DatasManager.Instance.speed.ToString();
    }

    public void UpdateHealth()
    {
        statsTexts[2].text = "生命值:+" + DatasManager.Instance.currentHealth.ToString();
    }

    public void UpdateAll()
    {
        UpdateDamage();
        UpdateHealth();
        UpdateSpeed();
    }

    public void CloseStats()
    {
        if (statsClose)
        {
            UpdateAll();
            statsClose = false;
            Time.timeScale = 1;
            statsCanvas.alpha = 0;
            statsCanvas.blocksRaycasts = false;
        }
        else
        {
            UpdateAll();
            statsClose = true;
            Time.timeScale = 0;
            statsCanvas.alpha = 1;
            statsCanvas.blocksRaycasts = true;
        }
    }
}
