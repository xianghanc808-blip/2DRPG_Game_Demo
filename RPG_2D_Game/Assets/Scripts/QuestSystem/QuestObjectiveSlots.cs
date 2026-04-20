using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestObjectiveSlots : MonoBehaviour
{
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text trackingText;

    //刷新任务条目——在任务条按钮中执行
    public void RefreshObjectives(string description, string progressText, bool isComplete)
    {
        objectiveText.text = description;
        trackingText.text = progressText;

        Color color = isComplete ? Color.gray : Color.black;
        objectiveText.color = color;
        trackingText.color = color;
    }
}
