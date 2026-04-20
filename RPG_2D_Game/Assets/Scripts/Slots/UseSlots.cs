using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UseSlots : MonoBehaviour
{
    public void ApplyItemEffects(ItemSo itemSO)
    {
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.ChangeHealth(itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth);
        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(itemSO.speed);
        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateDamage(itemSO.damage);
        if (itemSO.duration > 0)
            StartCoroutine(EffecctTimer(itemSO, itemSO.duration));
        
    }
    private IEnumerator EffecctTimer(ItemSo itemSO, float durarion)
    {
        yield return new WaitForSeconds(durarion);
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.ChangeHealth(-itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth);
        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(-itemSO.speed);
        if (itemSO.damage > 0)
            StatsManager.Instance.UpdateDamage(-itemSO.damage);
    }
}
