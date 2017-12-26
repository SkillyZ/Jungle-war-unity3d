using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button close;



    public override void OnEnter()
    {
        base.OnEnter();

        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.4f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);

        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(OnCloseClick);
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
