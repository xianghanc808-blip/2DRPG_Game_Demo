using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringQuestManager : MonoBehaviour
{
    public static SpringQuestManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private Dictionary<SpringQuestSO, Dictionary<SpringQuestObjective, int>> questProgress = new Dictionary<SpringQuestSO, Dictionary<SpringQuestObjective, int>>();
    private List<SpringQuestSO> completeQuests = new();

    private void OnEnable()
    {
        SpringQuestEvent.IsQuestComplete += IsQuestComplete;
    }

    private void OnDisable()
    {
        SpringQuestEvent.IsQuestComplete -= IsQuestComplete;
    }

    public bool IsQuestComplete(SpringQuestSO questSO)
    {
        if (!questProgress.TryGetValue(questSO, out var progressDiction))
            return false;

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }

        foreach (var objective in questSO.objectives)
        {
            if (progressDiction[objective] < objective.requiredAmount)
                return false;
        }
        return true;
    }


    public string GetProgressText(SpringQuestSO questSO, SpringQuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO, objective);

        if (currentAmount >= objective.requiredAmount)
            return "Complete";
        else if (objective.targetItem != null)
            return $"{currentAmount}/{objective.requiredAmount}";
        else
            return "In Progress";
    }

    public int GetCurrentAmount(SpringQuestSO questSO, SpringQuestObjective objective)
    {
        if(questProgress.TryGetValue(questSO, out Dictionary<SpringQuestObjective, int> objectiveDictionary))
            if(objectiveDictionary.TryGetValue(objective, out int amount))
                return amount;
        return 0;
    }

    public void UpdateObjectiveProgress(SpringQuestSO questSO, SpringQuestObjective objective)
    {
        if (!questProgress.ContainsKey(questSO))
            return;

        Dictionary<SpringQuestObjective, int> progressDictionary = questProgress[questSO];
        int newAmount = 0;

        if (objective.targetItem != null)
            newAmount = Inventory_Manager.Instance.GetItemQuantity(objective.targetItem);
        else if (objective.targetActor != null && SpringKillEnemyHistoryTrack.Instance.IsKilledEnenmy(objective.targetActor))
            newAmount = objective.requiredAmount;
        else if (objective.targetActor != null && SpringDialogueHistoryTrack.Instance.HasSpokenWith(objective.targetActor))
            newAmount = objective.requiredAmount;

            progressDictionary[objective] = newAmount;
    }

    public void AcceptQuest(SpringQuestSO questSO)
    {
        questProgress[questSO] = new Dictionary<SpringQuestObjective, int>();

        foreach (var objective in questSO.objectives)
        {
            UpdateObjectiveProgress(questSO, objective);
        }
    }

    public List<SpringQuestSO> GetActiveQeusts()
    {
        return new List<SpringQuestSO>(questProgress.Keys);
    }

    public bool ISQuestAccepted(SpringQuestSO questSO)
    {
        return questProgress.ContainsKey(questSO);
    }

    public void CompleteQuest(SpringQuestSO questSO)
    {
        questProgress.Remove(questSO);
        completeQuests.Add(questSO);

        foreach (var objective in questSO.objectives)
        {
            if (objective.targetItem != null && objective.requiredAmount > 0)
            {
                Inventory_Manager.Instance.RemoveItem(objective.targetItem, objective.requiredAmount);
            }
        }

        foreach (var reward in questSO.rewards)
        {
            Inventory_Manager.Instance.AddItem(reward.item, reward.quantity);
        }
    }
}
