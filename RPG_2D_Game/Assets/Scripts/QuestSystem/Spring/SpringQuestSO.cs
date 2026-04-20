using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSpringQuestSO", menuName = "Spring/QuestSO")]
public class SpringQuestSO : ScriptableObject
{
    public string LogSlotName;
    [TextArea]public string description;
    public int questLevel;

    public List<SpringQuestObjective> objectives;
    public List<SpringReward> rewards;
}

[System.Serializable]
public class SpringQuestObjective
{
    public string description;

    [SerializeField] private Object target;
    public Spring_ItemSO targetItem => target as Spring_ItemSO;
    public SpringActorSO targetActor => target as SpringActorSO;

    public int requiredAmount;
}
[System.Serializable]
public class SpringReward
{
    public Spring_ItemSO item;
    public int quantity;
}
