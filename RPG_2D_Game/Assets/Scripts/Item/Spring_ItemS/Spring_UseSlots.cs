using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_UseSlots : MonoBehaviour
{
    public void ApplyItemEffects(Spring_ItemSO itemSO)
    {
        if (itemSO.addCurrentHealth > 0)
            DatasManager.Instance.UpdateCurrentHealth(itemSO.addCurrentHealth);
        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));

    }

    private IEnumerator EffectTimer(Spring_ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (itemSO.addCurrentHealth > 0)
            DatasManager.Instance.UpdateCurrentHealth(- itemSO.addCurrentHealth);
    }


}
