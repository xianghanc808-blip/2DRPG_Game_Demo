using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpringDialogueManager : MonoBehaviour
{
    public static SpringDialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }


        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
        foreach(Button button in choicesButtons)
        {
            button.gameObject.SetActive(false);
        }

    }

    private SpringDialogue_SO currentDialogue;
    private int dialogueIndex;

    public bool isActiveDialogue;
    public bool isOverConversation;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Image portrait;
    public SpringActorSO enemySO;
    
    public CanvasGroup canvas;

    public Button[] choicesButtons;

    private float CollDownTimer = 1f;
    private float lastDialogueEndTime;

    public void StartDialogue(SpringDialogue_SO dialogue_SO)
    {
        if (Time.unscaledTime - lastDialogueEndTime < CollDownTimer)
            return;

        currentDialogue = dialogue_SO;
        dialogueIndex = 0;
        isActiveDialogue = true;
        ShowDialogue();
    }

    public void AdvanceDiaogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length )
        {
            ShowDialogue();
        }
        else
        {
            ShowChoices();
        }
    }

    public void ShowDialogue()
    {
        SpringDialogueLine line = currentDialogue.lines[dialogueIndex];
        SpringDialogueHistoryTrack.Instance.ReadNPCs(line.actorSO);

        portrait.sprite = line.actorSO.portiate;
        actorName.text = line.actorSO.actorName;
        dialogueText.text = line.text;

        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;

        dialogueIndex++;
    }

    public void ShowChoices()
    {
        ClearChoices();
        if (currentDialogue.options.Length > 0)
        {
            
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];
                choicesButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choicesButtons[i].gameObject.SetActive(true);
                choicesButtons[i].onClick.AddListener(() => ChoicesOption(option.nextDialogue));
            }
            EventSystem.current.SetSelectedGameObject(choicesButtons[0].gameObject);
        }
        else
        {
            if (currentDialogue.turnInQuestOnEnd != null&&
                SpringQuestManager.Instance.IsQuestComplete(currentDialogue.turnInQuestOnEnd))
            {
                SpringQuestEvent.OnQuestTurnInRequested?.Invoke(currentDialogue.turnInQuestOnEnd);
                EndDialogue();
            }
            else if (currentDialogue.offerQuestOnEnd != null)
            {
                EndDialogue();
                SpringQuestEvent.OnQuestOffer?.Invoke(currentDialogue.offerQuestOnEnd);
            }
            else
            {
                choicesButtons[0].gameObject.SetActive(true);
                choicesButtons[0].GetComponentInChildren<TMP_Text>().text = "½áÊø¶Ô»°";
                choicesButtons[0].onClick.AddListener(EndDialogue);
                EventSystem.current.SetSelectedGameObject(choicesButtons[0].gameObject);
            }
        }
    }

    public void ChoicesOption(SpringDialogue_SO dialogueSO)
    {
        if (dialogueSO == null)
            EndDialogue();
        else
        {
            ClearChoices();
            StartDialogue(dialogueSO);
        }
    }

    public void EndDialogue()
    {
        lastDialogueEndTime = Time.unscaledTime;

        SoundMusics.Instance.PlaySound("switch-a");
        dialogueIndex = 0;
        isActiveDialogue = false;
        if (currentDialogue.lines[0].actorSO == enemySO)
        {
            isOverConversation = true;
        }
        ClearChoices();

        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void ClearChoices()
    {
        foreach (var button in choicesButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }


}
