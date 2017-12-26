using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour {

    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } }

    private UIManager uiMng;
    private AudioManager audioMng;
    private CameraManager cameraMng;
    private PlayerManager playerMng;
    private RequestManager requestMng;
    private ClientManager clientMng;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);return;
        }

        _instance = this;
    }

    // Use this for initialization
    void Start () {
        InitManager();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitManager()
    {
        uiMng = new UIManager(this);
        audioMng = new AudioManager(this);
        cameraMng = new CameraManager(this);
        playerMng = new PlayerManager(this);
        requestMng = new RequestManager(this);
        clientMng = new ClientManager(this);

        uiMng.OnInit();
        audioMng.OnInit();
        cameraMng.OnInit();
        playerMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
    }

    private void DestoryManager()
    {
        uiMng.OnDestory();
        audioMng.OnDestory();
        cameraMng.OnDestory();
        playerMng.OnDestory();
        requestMng.OnDestory();
        clientMng.OnDestory();
    }

    private void OnDestroy()
    {
        DestoryManager();
    }

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }


    public void HandleReponse(ActionCode actionCode, string data)
    {
        requestMng.HandleReponse(actionCode, data);
    }
    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode, actionCode, data);
    }
}
