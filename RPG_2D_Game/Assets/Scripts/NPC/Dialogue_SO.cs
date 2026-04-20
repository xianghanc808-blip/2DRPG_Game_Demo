using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="DialogueSO", menuName ="Dialogue/DialogueNode")]
public class Dialogue_SO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    [Header("Quest Offer (Optional)")]
    public QuestSO offerQuestOnEnd;

    [Header("Completed Quest Requirements (Optional)")]
    public QuestSO[] requiredCompletedQuests;

    [Header("Quest Turn-In (Optional)")]
    public QuestSO turnInQuestOnEnd;

    [Header("Conditional Requirements (Optional)")]
    public Actor_SO[] requiredNPCs;
    public LocationSO[] requiredLocations;
    public ItemSo[] requiredItems;

    [Header("Control Flags")]
    public bool removeAfterPlay;
    public List<Dialogue_SO> removeTheseOnPlay;

    public bool IsConditionMet()
    {
        if (requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                if (!GameManager.Instance.DialogueHistoryTracker.HasSpokenWith(npc))
                    return false;
            }
        }

        if (requiredLocations.Length > 0)
        {
            foreach(var location in requiredLocations)
            {
                if (!GameManager.Instance.LocationHistoryTracker.HasVisited(location))
                    return false;
            }
        }

        if (requiredItems.Length > 0)
        {
            foreach (var item in requiredItems)
            {
                if (!ItemUIManager.Instance.HasItem(item))
                {
                    return false;
                }
            }
        }

        if (requiredCompletedQuests != null && requiredCompletedQuests.Length > 0)
        {
            foreach (var quest in requiredCompletedQuests)
            {
                if (!GameManager.Instance.QuestManager.IsQuestComplete(quest))
                {
                    return false;
                }
            }
        }

        return true;
    }
}

[System.Serializable]
public class DialogueLine
{
    public Actor_SO speeker;
    [TextArea(3, 5)] public string text;
}

[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public Dialogue_SO nextDialogue;
    public QuestSO offerQuest;
}