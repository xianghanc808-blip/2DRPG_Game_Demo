using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DialogueManager DialogueManager;
    public DialogueHistoryTrack DialogueHistoryTracker;
    public LocationHistoryTracks LocationHistoryTracker;
    public QuestManager QuestManager;

    [Header("Persistent Object")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        if (Instance != null)
        {
            CleanUpAndDestroy();
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
