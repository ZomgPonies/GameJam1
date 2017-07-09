using System;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    [SerializeField]
    private Button initiallySelectedButton;

    private Action callBackMethod;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void SetCallBackMethodOnClose(Action method)
    {
        callBackMethod = method;
    }

    public void CloseCredit()
    {
        Destroy(gameObject);
        if (callBackMethod != null) callBackMethod();
    }
}