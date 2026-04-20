using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen;

    private void Start()
    {
        UpdateAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAll();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAll();
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
        }
    }


    public void UpdateHealth()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "生命值:" + StatsManager.Instance.maxHealth;
    }

    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "速度:" + StatsManager.Instance.maxHealth;
    }

    public void UpdateDmage()
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "攻击力:" + StatsManager.Instance.damage;
    }

    public void UPdateForceback()
    {
        statsSlots[3].GetComponentInChildren<TMP_Text>().text = "防御力:" + StatsManager.Instance.guard;
    }
    public void UPdateStunTime()
    {
        statsSlots[4].GetComponentInChildren<TMP_Text>().text = "击晕:" + StatsManager.Instance.stunTime;
    }

    public void UpdateAll()
    {
        UpdateHealth();
        UpdateSpeed();
        UpdateDmage();
        UPdateForceback();
        UPdateStunTime();
    }
}
