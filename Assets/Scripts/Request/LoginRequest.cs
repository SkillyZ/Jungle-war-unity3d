using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest {

    private LoginPanel loginPanel;

	// Use this for initialization
	void Start () {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();

    }
	
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        //base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        loginPanel.OnLoginResponse(returnCode);
    }

}
