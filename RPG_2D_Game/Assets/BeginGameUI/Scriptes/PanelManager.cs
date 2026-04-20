using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager 
{
    private static PanelManager instance = new PanelManager();
    public static PanelManager Insrance => instance;

    private Stack<BasePanel> uiStack = new Stack<BasePanel>();
    private Dictionary<string, BasePanel> uiCache = new Dictionary<string, BasePanel>();

    private Transform uiRoot;

    public void Init(Transform root)
    {
        uiRoot = root;
    }

    public void PushPanel(string panelName)
    {
        if (uiStack.Count > 0) 
        {
            uiStack.Peek().OnPause();
        }

        BasePanel panel = GetPanel(panelName);
        panel.OnEnter();
        panel.Display(true);
        uiStack.Push(panel);
        
    }


    private BasePanel GetPanel(string panelName)
    {
        if (uiCache.TryGetValue(panelName, out var panel))
            return panel;

        GameObject prefab = Resources.Load<GameObject>($"UI/{panelName}");
        GameObject inst = Object.Instantiate(prefab, uiRoot);
        panel = inst.GetComponent<BasePanel>();

        uiCache.Add(panelName, panel);
        return panel;
    }
}
