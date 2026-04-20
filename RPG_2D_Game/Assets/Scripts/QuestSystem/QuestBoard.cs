using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    [SerializeField] private QuestSO questToOffer;
    [SerializeField] private QuestSO questToTurnIn;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.G))
        {
            bool canTurnIn = questToTurnIn != null && QuestEvent.IsQuestComplete?.Invoke(questToTurnIn) == true;

            if (canTurnIn)
            {
                QuestEvent.OnQuestTurnInRequested?.Invoke(questToTurnIn);
            }
            else
            {
                QuestEvent.OnQuestOfferRequested?.Invoke(questToOffer);
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
