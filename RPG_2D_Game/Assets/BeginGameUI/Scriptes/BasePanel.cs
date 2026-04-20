using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    public string Name { get; private set; }

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Name = this.GetType().Name;
    }

    public virtual void OnEnter() 
    {

    }

    public virtual void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnExit()
    {

    }

    public virtual void Display(bool isShow)
    {
        canvasGroup.alpha = isShow ? 1 : 0;
        canvasGroup.blocksRaycasts = isShow;
    }
}
