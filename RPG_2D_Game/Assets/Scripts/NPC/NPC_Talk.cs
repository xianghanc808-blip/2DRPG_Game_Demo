using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    public Animator interactAnim;

    public List<Dialogue_SO> conversations;
    public Dialogue_SO currentConversation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        QuestEvent.OnQuestAccepted += OnQuestAccepted_RemoveOfferings;
    }

    private void OnDestroy()
    {
        QuestEvent.OnQuestAccepted -= OnQuestAccepted_RemoveOfferings;
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        //anim.Play("Idle");
        interactAnim.Play("Open");
    }

    private void OnDisable()
    {
        //interactAnim.Play("Close");
        anim.Play("Walk");
        rb.isKinematic = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (GameManager.Instance.DialogueManager.isDialogueActive)
                GameManager.Instance.DialogueManager.AdvanceDialogue();
            else
            {
                if (GameManager.Instance.DialogueManager.CanSatrtDialogue())
                {
                    CheckForNewConversation();
                    GameManager.Instance.DialogueManager.StartDialogue(currentConversation);
                }
            } 
        }
    }

    private void CheckForNewConversation()
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            var convo = conversations[i];
            if (convo != null && convo.IsConditionMet())
            {
                currentConversation = convo;

                if (convo.removeAfterPlay)
                    conversations.RemoveAt(i);

                if (convo.removeTheseOnPlay != null && convo.removeTheseOnPlay.Count > 0)
                {
                    foreach (var toRemove in convo.removeTheseOnPlay)
                    {
                        conversations.Remove(toRemove);
                    }
                }
                break;
            }
        }
    }

    private void OnQuestAccepted_RemoveOfferings(QuestSO acceptedQuest)
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
