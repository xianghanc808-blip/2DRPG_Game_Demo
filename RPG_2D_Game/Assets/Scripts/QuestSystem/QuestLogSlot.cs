using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questLevelText;

    public QuestSO currentQuest;

    public QuestLogUI questLogUI;

    
    private void OnValidate()
    {
        if (currentQuest != null)
            SetQuest(currentQuest);
        else
            gameObject.SetActive(false);
    }

    //更新按钮条信息——OnValidate和接受按钮中执行
    public void SetQuest(QuestSO questSO)
    {
        currentQuest = questSO;

        questNameText.text = questSO.questName;
        questLevelText.text = "Lv." + questSO.questLevel;

        gameObject.SetActive(true);
    }
    
    //清除按钮条信息——在接受按钮中执行
    public void ClearSlot()
    {
        currentQuest = null;
        gameObject.SetActive(false);
    }

    //按钮按下更新细节面板信息——在任务条按钮中执行
    public void OnSlotClicked()
    {
        //传入当前按钮的任务并更新细节便面板信息
        questLogUI.HandleQuestClicked(currentQuest);
    }
}
