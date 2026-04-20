using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTrack : MonoBehaviour
{

    private readonly HashSet<Actor_SO> spokenNPCs = new HashSet<Actor_SO>();

    public void RecordNPC(Actor_SO actor_SO)
    {
        spokenNPCs.Add(actor_SO);
        Debug.Log("Just spoke to" + actor_SO.actorName);
    }

    public bool HasSpokenWith(Actor_SO actor_SO)
    {
        return spokenNPCs.Contains(actor_SO);
    }
}
