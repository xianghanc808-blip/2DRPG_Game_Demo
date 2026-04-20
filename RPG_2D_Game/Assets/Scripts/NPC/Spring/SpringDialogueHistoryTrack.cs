using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpringDialogueHistoryTrack : MonoBehaviour
{

    public static SpringDialogueHistoryTrack Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private readonly HashSet<SpringActorSO> spokenNpcs = new HashSet<SpringActorSO>();

    public void ReadNPCs(SpringActorSO actorSO)
    {
        spokenNpcs.Add(actorSO);
        Debug.Log("add+" + actorSO);
    }

    public bool HasSpokenWith(SpringActorSO actorSO)
    {
        return spokenNpcs.Contains(actorSO);
    }
}
