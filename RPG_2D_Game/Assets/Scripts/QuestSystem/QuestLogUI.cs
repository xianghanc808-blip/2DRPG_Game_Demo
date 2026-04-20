using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;

    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questDescriptionText;
    [SerializeField] private QuestObjectiveSlots[] objectiveSlots;
    [SerializeField] private QuestRewardSlots[] rewardSlots;

    private QuestSO questSO;

    [SerializeField] private QuestSO noAvailableQuestSO;
    [SerializeField] private QuestLogSlot[] questSlots;

    [SerializeField] private CanvasGroup questCanvas;

    [SerializeField] private CanvasGroup acceptCanvasGroup;
    [SerializeField] private CanvasGroup declineCanvasGroup;
    [SerializeField] private CanvasGroup completeCanvasGroup;


    private void OnEnable()
    {
        QuestEvent.OnQuestOfferRequested += ShowQuestOffer;
        QuestEvent.OnQuestTurnInRequested += ShowQuestTurnIn;
    }
    private void OnDisable()
    {
        QuestEvent.OnQuestOfferRequested -= ShowQuestOffer;
        QuestEvent.OnQuestTurnInRequested -= ShowQuestTurnIn;
    }

    public void ShowQuestTurnIn(QuestSO incomingQuestSO)
    {
        questSO = incomingQuestSO;

        HandleQuestClicked(questSO);

        SetCanvasState(completeCanvasGroup, true);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        SetCanvasState(questCanvas, true);
    }

    #region ShowQuestMethed
    public void ShowQuestOffer(QuestSO incomingQuestSO)
    {
        if (questManager.ISQuestAccepted(incomingQuestSO) || questManager.GetCompleteQuest(incomingQuestSO))
        {
            questSO = noAvailableQuestSO;
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

        HandleQuestClicked(questSO);
        SetCanvasState(questCanvas, true);
        
    }
    #endregion

    #region On Button Clicked Methods

    //接受任务按钮
    public void OnAcceptQuestClicked()
    {

        QuestEvent.OnQuestAccepted?.Invoke(questSO);

        questManager.AcceptQuest(questSO);
        SetCanvasState(completeCanvasGroup, false);
        SetCanvasState(acceptCanvasGroup, false);
        SetCanvasState(declineCanvasGroup, false);
        RefreshQuestList();
        HandleQuestClicked(questSO);
    }

    public void OnDeclineQuestClicked()
    {
        SetCanvasState(questCanvas, false);
    }

    public void OnCompleteQuestClicked()
    {
        questManager.CompleteQuest(questSO);

        RefreshQuestList();
        HandleQuestClicked(noAvailableQuestSO);
        SetCanvasState(completeCanvasGroup, false);
    }
    #endregion

    private void SetCanvasState(CanvasGroup group, bool activate)
    {
        group.alpha = activate ? 1 : 0;
        group.blocksRaycasts = activate;
        group.interactable = activate;
    }

    public void RefreshQuestList()
    {
        List<QuestSO> activeQuests = questManager.GetActiveQuests();

        for (int i = 0; i < questSlots.Length; i++)
        {
            if (i < activeQuests.Count)
            {
                questSlots[i].SetQuest(activeQuests[i]);
            }
            else
            {
                questSlots[i].ClearSlot();
            }
        }
    }

    //任务条按钮响应时所执行的方法——在任务条按钮中执行
    public void HandleQuestClicked(QuestSO questSO)
    {
        this.questSO = questSO;
        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        //展示目标任务进度及其要求
        DisplayObjectives();
        //展示任务奖励
        DisplayRewards();
    }

    private void DisplayObjectives()
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if (i < questSO.objectives.Count)
            {
                var objective = questSO.objectives[i];
                //任务管理器中更新任务进度
                questManager.UpdateObjectiveProgress(questSO, objective);
                //任务管理器中获取当前进度值
                int currentAmount = questManager.GetCurrentAmount(questSO, objective);
                //任务管理器中获取当前进度信息
                string progress = questManager.GetProgressText(questSO, objective);
                bool isComplete = currentAmount >= objective.requiredAmount;

                objectiveSlots[i].gameObject.SetActive(true);
                //刷新任务条目
                objectiveSlots[i].RefreshObjectives(objective.description, progress, isComplete);
            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void DisplayRewards()
    {
        for(int i = 0; i < rewardSlots.Length; i++)
        {
            if(i < questSO.rewards.Count)
            {
                var reward = questSO.rewards[i];
                rewardSlots[i].DisplayReward(reward.itemSO.slotIcon, reward.quantity);
                rewardSlots[i].gameObject.SetActive(true);
            }
            else
            {
                rewardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
