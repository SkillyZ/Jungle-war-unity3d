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

    public void AddRequest(RequestCode requestCode, BaseRequest request)
    {
        requestMng.AddRequest(requestCode, request);
    }

    public void RemoveRequest(RequestCode requestCode)
    {
        requestMng.RemoveRequest(requestCode);
    }


    public void HandleReponse(RequestCode requestCode, string data)
    {
        requestMng.HandleReponse(requestCode, data);
    }
    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }
}
