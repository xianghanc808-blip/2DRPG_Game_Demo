using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringQuestBoard : MonoBehaviour
{
    public SpringQuestSO questToOffer;
    public SpringQuestSO questToTurnIn;
    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.P))
        {
            bool canTurnIn = questToTurnIn != null && SpringQuestEvent.IsQuestComplete?.Invoke(questToTurnIn) == true;

            if (canTurnIn)
            {
                SpringQuestEvent.OnQuestTurnInRequested?.Invoke(questToTurnIn);
            }
            else
            {
                SpringQuestEvent.OnQuestOffer?.Invoke(questToOffer);
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
