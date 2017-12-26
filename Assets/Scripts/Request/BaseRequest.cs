using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRequest : MonoBehaviour {

    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade gameFacade;
    public void SetFacade(GameFacade gameFacade)
    {
        this.gameFacade = gameFacade;
    }

	// Use this for initialization
	public virtual void Awake () {
        GameFacade.Instance.AddRequest(actionCode, this);
        gameFacade = GameFacade.Instance;

    }

    protected void SendRequest(string data)
    {
        gameFacade.SendRequest(requestCode, actionCode, data);
    }

    public virtual void SendRequest() { }
    public virtual void OnResponse(string data) { }

	public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
