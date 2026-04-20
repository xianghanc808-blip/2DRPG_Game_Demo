using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringQuestObjectiveSlots : MonoBehaviour
{
    public TMP_Text objectiveText;
    public TMP_Text trackingText;


    public void RefreshObjectives(string description, string progressText, bool isComplete)
    {
        objectiveText.text = description;
        trackingText.text = progressText;

        Color color = isComplete ? Color.gray : Color.black;
        objectiveText.color = color;
        trackingText.color = color;
    }
}
