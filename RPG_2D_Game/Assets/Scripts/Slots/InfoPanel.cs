using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public CanvasGroup infoPanel;

    public TMP_Text itemNameText;

    [Header("Stat Fialds")]
    public TMP_Text[] statTexts;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSo itemSO)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;

        List<string> stats = new List<string>();
        if (itemSO.currentHealth > 0) stats.Add("生命值:+" + itemSO.currentHealth.ToString());
        if (itemSO.damage > 0) stats.Add("伤害:+" + itemSO.damage.ToString());
        if (itemSO.speed > 0) stats.Add("速度:+" + itemSO.speed.ToString());
        if (itemSO.duration > 0) stats.Add("可持续时间:+" + itemSO.duration.ToString());


        if (stats.Count <= 0)
            return;


        for(int i = 0; i < statTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statTexts[i].text = stats[i];
                statTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statTexts[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);

        infoPanelRect.position = mousePosition + offset;
    }
}
