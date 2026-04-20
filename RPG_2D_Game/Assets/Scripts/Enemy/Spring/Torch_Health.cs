using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[ExecuteAlways]
public class Torch_Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Texture2D bkTextureNowHP;
    public SpringActorSO enemySO;
    public int Addexperience = 10;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public GameObject droppRewardItem;
    public Spring_ItemSO rewardItemSO;

    private Rect nowRect;
    private float showTime;


    public static Action<int,Vector3> OnDamage;
    private Vector3 enemyPos;
    private void OnGUI()
    {
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

            screenPos.y = Screen.height - screenPos.y;

            nowRect.x = screenPos.x - 50;
            nowRect.y = screenPos.y - 150;
            nowRect.width = (float)currentHealth / maxHealth * 100f;
            nowRect.height = 200;
            GUI.DrawTexture(nowRect, bkTextureNowHP);
            
        }
    }

    private void Update()
    {
        enemyPos = Camera.main.WorldToScreenPoint(this.transform.position);
    }

    public void ChangeHealth(int amount)
    {
        if (SpringDialogueManager.Instance.isOverConversation)
        {
            showTime = 3f;
            currentHealth -= amount;
            OnDamage?.Invoke(amount, enemyPos);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth <= 0)
            {
                OnMonsterDefeated(Addexperience);
                SpringKillEnemyHistoryTrack.Instance.KillEnemy(enemySO);
                DropRewardItems(rewardItemSO, 1);
                Destroy(gameObject);
                DatasManager.Instance.experiencePoints += Addexperience;
            }
        }
    }
    public void DropRewardItems(Spring_ItemSO itemSO, int quantity)
    {
        droppRewardItem.GetComponentInChildren<SpriteRenderer>().sprite = itemSO.mapIcon;
        SpringItem item = Instantiate(droppRewardItem, this.transform.position, Quaternion.identity).GetComponent<SpringItem>();
        item.Initialize(itemSO, quantity);
    }
}
