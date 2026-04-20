using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringKillEnemyHistoryTrack : MonoBehaviour
{
    public static SpringKillEnemyHistoryTrack Instance;

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

    public readonly HashSet<SpringActorSO> KillEnemys = new HashSet<SpringActorSO>();

    public void KillEnemy(SpringActorSO enemy)
    {
        KillEnemys.Add(enemy);
        Debug.Log("Kill"+enemy);
    }


    public bool IsKilledEnenmy(SpringActorSO enemy)
    {
        return KillEnemys.Contains(enemy);
    }
}
