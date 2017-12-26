﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button close;
    //private Button loginButton;
    //private Button registerButton;
    private InputField username;
    private InputField password;
    private LoginRequest loginRequest;


    public override void OnEnter()
    {
        base.OnEnter();

        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.4f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);

        loginRequest = GetComponent<LoginRequest>();
        username = transform.Find("UserLabel/UsernameInput").GetComponent<InputField>();
        password = transform.Find("PssswordLabel/passwordInput").GetComponent<InputField>();
        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(OnCloseClick);

        Button loginButton = transform.Find("LoginButton").GetComponent<Button>();
        Button registerButton = transform.Find("RegisterButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton.onClick.AddListener(OnRegisterClick);

    }

    private void OnLoginClick()
    {
        string msg = "";
        if (string.IsNullOrEmpty(username.text))
        {
            msg += "用户名不能为空.";
        }

        if (string.IsNullOrEmpty(password.text))
        {
            msg += "密码不能为空";
        }

        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        loginRequest.SendRequest(username.text, password.text);
    }

    private void OnRegisterClick()
    {

    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.5f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0 , 0), 0.5f);
        tweener.OnComplete(() => uiMng.PopPanel());
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}
