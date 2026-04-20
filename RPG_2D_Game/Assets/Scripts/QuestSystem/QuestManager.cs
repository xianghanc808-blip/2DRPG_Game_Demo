using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();
    private List<QuestSO> completedQuests = new();

    private void OnEnable()
    {
        QuestEvent.IsQuestComplete += IsQuestComplete;
    }

    private void OnDisable()
    {
        QuestEvent.IsQuestComplete -= IsQuestComplete;
    }

    #region Quest Accept Logic
    public bool ISQuestAccepted(QuestSO questSO)
    {
        return questProgress.ContainsKey(questSO);
    }

    public List<QuestSO> GetActiveQuests()
    {
        return new List<QuestSO>(questProgress.Keys);
    }

    public void AcceptQuest(QuestSO questSO)
    {
        questProgress[questSO] = new Dictionary<QuestObjective, int>();

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }
    }
    #endregion

    public bool IsQuestComplete(QuestSO questSO)
    {
        if (!questProgress.TryGetValue(questSO, out var progressDict))
            return false;

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }

        foreach (var objective in questSO.objectives)
        {
            if (progressDict[objective] < objective.requiredAmount)
                return false;
        }

        return true;
    }

    public void CompleteQuest(QuestSO questSO)
    {
        questProgress.Remove(questSO);
        completedQuests.Add(questSO);

        foreach (var reward in questSO.rewards)
        {
            ItemUIManager.Instance.AddItem(reward.itemSO, reward.quantity);
        }

        foreach (var reward in questSO.rewards)
        {
            ItemUIManager.Instance.AddItem(reward.itemSO, reward.quantity);
        }
    }

    public bool GetCompleteQuest(QuestSO questSO)
    {
        return completedQuests.Contains(questSO);
    }

    public void UpdateObjectiveProgress(QuestSO questSO, QuestObjective objective)
    {
        if (!questProgress.ContainsKey(questSO))
            return;

        var progressDictionary = questProgress[questSO];
        int newAmount = 0;

        if (objective.targetItem != null)
            newAmount = ItemUIManager.Instance.GetItemQuantity(objective.targetItem);
        else if (objective.targetLocation != null && GameManager.Instance.LocationHistoryTracker.HasVisited(objective.targetLocation))
            newAmount = objective.requiredAmount;
        else if (objective.targetNPC != null && GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(objective.targetNPC))
            newAmount = objective.requiredAmount;

        progressDictionary[objective] = newAmount;
    }

    public string GetProgressText(QuestSO questSO, QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);

        if (currentAmount >= objective.requiredAmount)
            return "Compete";
        else if (objective.targetItem != null)
            return $"{currentAmount}/{objective.requiredAmount}";
        else
            return "In Progress";
    }

    public int GetCurrentAmount(QuestSO questSO, QuestObjective objective)
    {
        if (questProgress.TryGetValue(questSO, out var objectiveDirectionary))
            if (objectiveDirectionary.TryGetValue(objective, out int amount))
                return amount;
        return 0;
    }
}
