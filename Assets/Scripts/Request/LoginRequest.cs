﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest {

	// Use this for initialization
	void Start () {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
	}
	
    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }
	
}
