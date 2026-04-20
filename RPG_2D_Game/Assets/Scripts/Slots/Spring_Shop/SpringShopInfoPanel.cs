using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringShopInfoPanel : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text itemNameText;
    [Header("State Fialds")]
    public TMP_Text[] statsTexts;
    private RectTransform inforPanelRect;

    private void Awake()
    {
        inforPanelRect = GetComponent<RectTransform>();
    }

    public void ShowItemInfo(Spring_ItemSO itemSO)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSO.itemName;

        List<string> stats = new List<string>();
        if (itemSO.addCurrentHealth > 0) stats.Add("生命值:+" + itemSO.addCurrentHealth.ToString());
        if (itemSO.addDamage > 0) stats.Add("伤害:+" + itemSO.addDamage.ToString());
        if (itemSO.addSpeed > 0) stats.Add("速度:+" + itemSO.addSpeed.ToString());
        if (itemSO.duration > 0) stats.Add("可持续时间:" + itemSO.duration.ToString());
        if (stats.Count <= 0)
            return;

        for (int i = 0; i < statsTexts.Length; i++)
        {
            if (i < stats.Count)
            {
                statsTexts[i].text = stats[i];
                statsTexts[i].gameObject.SetActive(true);
            }
            else
            {
                statsTexts[i].gameObject.SetActive(false);
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
        Vector3 offset = new Vector3(150, -150, 0);

        inforPanelRect.position = mousePosition + offset;
    }
}
