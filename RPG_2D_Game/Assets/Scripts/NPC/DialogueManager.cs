using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("UI Reference")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;

    public bool isDialogueActive;
    public Button[] choiceButtons;

    private Dialogue_SO currentDialogue;
    private int dialogueIndex;

    private float lastDialogueEndTime;
    private float dialogueColldown = 0.1f;
    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public bool CanSatrtDialogue()
    {
        return Time.unscaledTime - lastDialogueEndTime >= dialogueColldown;
    }


    public void StartDialogue(Dialogue_SO dialogue_SO)
    {
     
        currentDialogue = dialogue_SO;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
            ShowDialogue();
        else
            ShowChoices();
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        GameManager.Instance.DialogueHistoryTracker.RecordNPC(line.speeker);

        portrait.sprite = line.speeker.portrait;
        //actorName.text = line.speeker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }

    private void ShowChoices()
    {
        ClearChoices();
        if (currentDialogue.options.Length > 0)
        {
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));
            }
            EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
        }
        else
        {
            if(currentDialogue.turnInQuestOnEnd != null &&
                GameManager.Instance.QuestManager.IsQuestComplete(currentDialogue.turnInQuestOnEnd))
            {
                QuestEvent.OnQuestTurnInRequested?.Invoke(currentDialogue.turnInQuestOnEnd);
                EndDialogue();
            }
            else if (currentDialogue.offerQuestOnEnd != null)
            {
                EndDialogue();
                QuestEvent.OnQuestOfferRequested?.Invoke(currentDialogue.offerQuestOnEnd);
            }
            else
            {
                choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "½áÊø";
                choiceButtons[0].onClick.AddListener(EndDialogue);
                choiceButtons[0].gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
            }
        }
    }

    private void ChooseOption(Dialogue_SO dialogue_SO)
    {
        if (dialogue_SO == null)
            EndDialogue();
        else
        {
            ClearChoices();
            StartDialogue(dialogue_SO);
        }
    }

    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        lastDialogueEndTime = Time.unscaledTime;
    }

    private void ClearChoices()
    {
        foreach(var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
