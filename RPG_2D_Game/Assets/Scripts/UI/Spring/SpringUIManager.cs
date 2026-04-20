using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpringUIManager : MonoBehaviour
{
    public CanvasGroup stateCanvas;
    public CanvasGroup shopCanvas;
    public CanvasGroup taskCanvas;
    public CanvasGroup closeAllCanvas;
    public SpringQuestLogUI springQuestLogUI;
    public SpringQuestSO nonQuestSO;

    public void OnToggleCanvasGroup(CanvasGroup canvas )
    {
        SetCanvasGroupState(stateCanvas, false);
        SetCanvasGroupState(shopCanvas, false);
        SetCanvasGroupState(taskCanvas, false);

        if(canvas == taskCanvas)
        {
            springQuestLogUI.HandleQuestOnClicked(nonQuestSO);
            springQuestLogUI.CloseAffiliatedButton();
        }

        SetCanvasGroupState(canvas, true);
        SetCanvasGroupState(closeAllCanvas, true);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void OnCloseAllPanel()
    {
        SetCanvasGroupState(stateCanvas, false);
        SetCanvasGroupState(shopCanvas, false);
        SetCanvasGroupState(taskCanvas, false);
        SetCanvasGroupState(closeAllCanvas, false);
        SoundMusics.Instance.PlaySound("switch-a");
    }

    public void SetCanvasGroupState(CanvasGroup canvas, bool isActive )
    {
        canvas.alpha = isActive ? 1 : 0;
        canvas.interactable = isActive;
        canvas.blocksRaycasts = isActive;

    }
}
