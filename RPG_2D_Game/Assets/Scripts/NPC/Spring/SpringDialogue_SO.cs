using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Spring/DialogueSO")]
public class SpringDialogue_SO : ScriptableObject
{
    public SpringDialogueLine[] lines;
    public SpringDialogueOption[] options;

    [Header("Quest Offer (Optional)")]
    public SpringQuestSO offerQuestOnEnd;

    [Header("Completed Quest Requirements (Optional)")]
    public SpringQuestSO[] requiredCompletedQuests;

    [Header("Quest Turn-In (Optional)")]
    public SpringQuestSO turnInQuestOnEnd;

    [Header("Conditioanl Requirements (Optional)")]
    public SpringActorSO[] requireNPCs;
    public SpringActorSO[] requireKillEnemy;
    public Spring_ItemSO[] requireItems;

    [Header("Control FLags")]
    public bool removeAfterPlay;
    public List<SpringDialogue_SO> removeTheseOnPlay;

    public bool IsConditionMet()
    {
        if (requireNPCs.Length > 0)
        {
            foreach (var npc in requireNPCs)
            {
                if (!SpringDialogueHistoryTrack.Instance.HasSpokenWith(npc))
                {
                    return false;
                }
            }
        }
        if (requireKillEnemy.Length > 0)
        {
            foreach (var enemy in requireKillEnemy)
            {
                if (!SpringKillEnemyHistoryTrack.Instance.IsKilledEnenmy(enemy))
                {
                    return false;
                }
            }
        }
        if (requireItems.Length > 0)
        {
            foreach (var item in requireItems)
            {
                if (Inventory_Manager.Instance.HasItems(item))
                {
                    return false;
                }
            }
        }
        if(requiredCompletedQuests != null && requiredCompletedQuests.Length > 0)
        {
            foreach (var quest in requiredCompletedQuests)
            {
                if (!SpringQuestManager.Instance.IsQuestComplete(quest))
                {
                    return false;
                }
            }
        }

        return true;
    }
}

[System.Serializable]
public class SpringDialogueLine
{
    public SpringActorSO actorSO;
    [TextArea(3, 5)] public string text;
}

[System.Serializable]
public class SpringDialogueOption
{
    public string optionText;
    public SpringDialogue_SO nextDialogue;
    public SpringQuestSO quest;
}
