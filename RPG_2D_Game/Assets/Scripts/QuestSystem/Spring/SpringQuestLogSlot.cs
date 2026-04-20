using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringQuestLogSlot : MonoBehaviour
{
    //…Ë÷√
    public TMP_Text questLogSlotName;

    // ‰»Î
    private SpringQuestSO currentQuestSO;

    public SpringQuestLogUI questLogUI;

    private void OnValidate()
    {
        if (currentQuestSO != null)
        {
            SetQuestLogSlot(currentQuestSO);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetQuestLogSlot(SpringQuestSO questSO)
    {
        currentQuestSO = questSO;
        gameObject.SetActive(true);
        questLogSlotName.text = questSO.LogSlotName + "Level:" + questSO.questLevel;
    }
    
    public void ClearSlot()
    {
        currentQuestSO = null;
        gameObject.SetActive(false);
    }


    public void OnLogSlotCkick()
    {
        questLogUI.HandleQuestOnClicked(currentQuestSO);
        SoundMusics.Instance.PlaySound("switch-a");
    }
}
