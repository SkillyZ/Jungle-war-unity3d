using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RegisterPanel : BasePanel
{
    private Button close;
    private InputField username;
    private InputField password;
    private InputField rpassword;
    private RegisterRequest registerRequest;

    private void Start()
    {
        gameObject.SetActive(true);
        registerRequest = GetComponent<RegisterRequest>();
        username = transform.Find("UserLabel/UsernameInput").GetComponent<InputField>();
        password = transform.Find("PssswordLabel/passwordInput").GetComponent<InputField>();
        rpassword = transform.Find("RePssswordLabel/RePasswordInput").GetComponent<InputField>();
        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(OnCloseClick);

        Button registerButton = transform.Find("RegisterButton").GetComponent<Button>();
        registerButton.onClick.AddListener(OnRegisterClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();

        //transform.localScale = Vector3.zero;
        //transform.DOScale(1, 0.4f);
        //transform.localPosition = new Vector3(1000, 0, 0);
        //transform.DOLocalMove(Vector3.zero, 0.4f);

    }

    private void OnRegisterClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(username.text))
        {
            msg += "用户名不能为空.";
        }

        if (string.IsNullOrEmpty(password.text))
        {
            msg += "密码不能为空";
        }

        if (string.IsNullOrEmpty(rpassword.text))
        {
            msg += "确认密码不能为空";
        }


        if (rpassword.text != password.text)
        {
            msg += "请确认两次输入密码一致";
        }

        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        registerRequest.SendRequest(username.text, password.text);
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.5f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.5f);
        tweener.OnComplete(() => uiMng.PopPanel());
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {

            uiMng.ShowMessageSync("成功");
        }
        else
        {
            uiMng.ShowMessageSync("用户名重复， 请重新输入");
        }
    }

    public override void OnPause()
    {
        base.OnPause();
        gameObject.SetActive(false);

    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

}
