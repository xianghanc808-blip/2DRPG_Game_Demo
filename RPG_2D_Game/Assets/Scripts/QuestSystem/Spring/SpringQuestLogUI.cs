using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpringQuestLogUI : MonoBehaviour
{
    
    public SpringQuestObjectiveSlots[] objectiveSlots;
    public SpringQuestRewardSlots[] rewardSlots;
    public SpringQuestLogSlot[] questLogSlots;
    public SpringQuestManager questManager;
    public SpringQuestSO nonAvailableQuestSO;

    private SpringQuestSO questSO;
    public TMP_Text description;
    public bool isClose;

    [Header("canvasGroup")]
    public CanvasGroup questCanvas;
    public CanvasGroup acceptCanvasGroup;
    public CanvasGroup declineCanvasGroup;
    public CanvasGroup completeCanvasGroup;
    public CanvasGroup buttonCloseQuestPanel;


    public void OnColseOrOpen()
    {
        if (isClose)
        {
            SetCanvasState(questCanvas, false);
            isClose = false;
        }
        else
        {
            SetCanvasState(questCanvas, true);
            isClose = true;
        }
    }



    private void SetCanvasState(CanvasGroup group, bool active)
    {
        group.alpha = active ? 1 : 0;
        group.blocksRaycasts = active;
        group.interactable = active;
    }

    public void CloseAffiliatedButton()
    {
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(completeCanvasGroup, false);
    }

    private void OnEnable()
    {
        SpringQuestEvent.OnQuestTurnInRequested += ShowQuestTurnIn;
        SpringQuestEvent.OnQuestOffer += ShowQuestOffer;
    }

    private void OnDisable()
    {
        SpringQuestEvent.OnQuestTurnInRequested -= ShowQuestTurnIn;
        SpringQuestEvent.OnQuestOffer -= ShowQuestOffer;

    }

    public void ShowQuestOffer(SpringQuestSO incomingQuestSO)
    {
        if (questManager.ISQuestAccepted(incomingQuestSO))
        {
            questSO = nonAvailableQuestSO;
            SetCanvasState(acceptCanvasGroup, false);
            SetCanvasState(declineCanvasGroup, true);
            SetCanvasState(completeCanvasGroup, false);
        }
        else
        {
            questSO = incomingQuestSO;
            SetCanvasState(acceptCanvasGroup, true);
            SetCanvasState(declineCanvasGroup, true);
            SetCanvasState(completeCanvasGroup, false);
        }
        HandleQuestOnClicked(questSO);
        SetCanvasState(questCanvas, true);
        SetCanvasState(buttonCloseQuestPanel, true);
    }

    public void ShowQuestTurnIn(SpringQuestSO incomingQuestSO)
    {
        questSO = incomingQuestSO;

        HandleQuestOnClicked(questSO);

        SetCanvasState(completeCanvasGroup, true);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(questCanvas, true);
        SetCanvasState(buttonCloseQuestPanel, true);
    }

    public void OnAcceptQuestClicked()
    {
        SpringQuestEvent.OnQuestAccepted?.Invoke(questSO);

        questManager.AcceptQuest(questSO);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(completeCanvasGroup, false);
        RefreshQuestList();
        HandleQuestOnClicked(questSO);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void OnCompletedClicked()
    {
        questManager.CompleteQuest(questSO);

        RefreshQuestList();
        HandleQuestOnClicked(nonAvailableQuestSO);
        SetCanvasState(completeCanvasGroup, false);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void RefreshQuestList()
    {
        List<SpringQuestSO> activeQuests = questManager.GetActiveQeusts();

        for (int i = 0; i < questLogSlots.Length; i++)
        {
            if (i < activeQuests.Count)
            {
                questLogSlots[i].SetQuestLogSlot(activeQuests[i]);
            }
            else
            {
                questLogSlots[i].ClearSlot();
            }
        }
    }

    public void OnDeclineQuestClicked()
    {
        SetCanvasState(questCanvas, false);
        SetCanvasState(buttonCloseQuestPanel, false);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void HandleQuestOnClicked(SpringQuestSO springQuestSO)
    {
        this.questSO = springQuestSO;
        description.text = springQuestSO.description;
        foreach (var objective in springQuestSO.objectives)
        {
            questManager.UpdateObjectiveProgress(springQuestSO, objective);
            //Debug.Log($"Objective:{objective.description}{questManager.GetProgressText(springQuestSO, objective)}");
        }
        DisplayObjectives();
        DisplayRewardSlots();
    }

    private void DisplayObjectives()
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if(i < questSO.objectives.Count)
            {
                SpringQuestObjective objective = questSO.objectives[i];

                questManager.UpdateObjectiveProgress(questSO, objective);

                int currentAmount = questManager.GetCurrentAmount(questSO, objective);
                string progress = questManager.GetProgressText(questSO, objective);
                bool isComplete = currentAmount >= objective.requiredAmount;
                
                objectiveSlots[i].gameObject.SetActive(true);

                objectiveSlots[i].RefreshObjectives(objective.description, progress, isComplete);
            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void DisplayRewardSlots()
    {
        for (int i = 0; i < rewardSlots.Length; i++)
        {
            if(i < questSO.rewards.Count)
            {
                SpringReward reward = questSO.rewards[i];
                rewardSlots[i].gameObject.SetActive(true);
                rewardSlots[i].DisplayReward(reward.item.slotIcon,reward.quantity);
            }
            else
            {
                rewardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
