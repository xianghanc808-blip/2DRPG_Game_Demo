using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpringNPC_Talk : MonoBehaviour
{


    private SpringDialogue_SO currentConversation;
    private bool canClickSpcae;
    public List<SpringDialogue_SO> conversations;

    public static Action OnInteractedTips;
    public static Action OnEndInteractivedTips;
    public static Action OnAIIsStop;
    public static Action OnAIRoam;



    private void Start()
    {
        SpringQuestEvent.OnQuestAccepted += OnQuestAccepted_RemoveOfferings;

    }

    private void OnDestroy()
    {
        SpringQuestEvent.OnQuestAccepted -= OnQuestAccepted_RemoveOfferings;
    }


    private void Update()
    {
        if (canClickSpcae)
        {

            EventSystem.current.SetSelectedGameObject(null);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnEndInteractivedTips?.Invoke();
                if (SpringDialogueManager.Instance.isActiveDialogue)
                {
                    SpringDialogueManager.Instance.AdvanceDiaogue();
                }
                else
                {
                    CheckForNewConversation();
                    SpringDialogueManager.Instance.StartDialogue(currentConversation);
                    currentConversation = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (conversations.Count > 0)
            {
                canClickSpcae = true;
                OnAIIsStop?.Invoke();
                OnInteractedTips?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canClickSpcae = false;
            OnAIRoam?.Invoke();
            OnEndInteractivedTips?.Invoke();
        }
    }

    public void CheckForNewConversation()
    {
        if(conversations.Count > 0)
        {
            for (int i = 0; i < conversations.Count; i++)
            {
                SpringDialogue_SO conversation = conversations[i];

                if(conversation != null && conversation.IsConditionMet())
                {
                    currentConversation = conversation;

                    if (conversation.removeAfterPlay)
                    {
                        conversations.RemoveAt(i);
                    }
                        

                    if (conversation.removeTheseOnPlay != null && conversation.removeTheseOnPlay.Count > 0)
                    {
                        foreach(var toRemove in conversation.removeTheseOnPlay)
                        {
                            conversations.Remove(toRemove);
                        }
                    }
                    break;
                }
            }
        }
    }

    private void OnQuestAccepted_RemoveOfferings(SpringQuestSO acceptedQuest)
    {
        for (int i = conversations.Count - 1; i >= 0; i--)
        {
            var convo = conversations[i];
            if (convo == null)
                continue;

            if (convo.offerQuestOnEnd == acceptedQuest)
                conversations.RemoveAt(i);

        }
    }
}
