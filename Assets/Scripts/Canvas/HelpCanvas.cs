using System;
using UnityEngine;
using UnityEngine.UI;

public class HelpCanvas : MonoBehaviour
{
    [SerializeField]
    private Button initiallySelectedButton;

    private Action callBackMethod;
    private GameObject disabledCanvas;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void SetCallBackMethodOnClose(Action method)
    {
        callBackMethod = method;
    }

    public void SetDisabledCanvas(GameObject disabledCanvas)
    {
        this.disabledCanvas = disabledCanvas;
        this.disabledCanvas.SetActive(false);
    }

    public void CloseHelp()
    {
        Destroy(gameObject);
        disabledCanvas.SetActive(true);
        if (callBackMethod != null) callBackMethod();
    }
}
