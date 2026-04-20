using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    [SerializeField] private TMP_InputField inputAccount;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private Button btnLogin;
    [SerializeField] private Toggle toggleRemeber;

    public override void OnEnter()
    {
        base.OnEnter();
        InitView();
        AddListeners();
    }


    private void InitView()
    {
        btnLogin.interactable = false;
    }

    private void AddListeners()
    {
        

    }




   
}
