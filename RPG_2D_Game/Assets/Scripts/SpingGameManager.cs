using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpingGameManager : MonoBehaviour
{
    public static SpingGameManager Instance;


    public GameObject[] persistentObjects;
    public GameObject[] dontPersistenObjects;
    private void Awake()
    {
        if(Instance != null)
        {
            ClearUpAndDestroy();
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }

    public void MarkPersistentObjects()
    {
        foreach (GameObject objects in persistentObjects)
        {
            if (objects != null)
            {
                DontDestroyOnLoad(objects);
            }
        }
    }

    public void ClearUpAndDestroy()
    {
        foreach(GameObject objects in persistentObjects)
        {
            if(objects != null)
            {
                Destroy(objects);
            }
            Destroy(gameObject);
        }

        foreach(GameObject objects in dontPersistenObjects)
        {
            foreach(var itemSO in DatasManager.Instance.hasItemNow)
            {
                if (objects.gameObject.GetComponent<SpringItem>().itemSO == itemSO)
                {
                    Destroy(objects);
                }
            }
            
        }
    }
}
