using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmCanvas : MonoBehaviour
{
    [SerializeField]
    private Button initiallySelectedButton;

    private Action acceptMethod;
    private Action refuseMethod;

    private GameObject disabledCanvas;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void SetAcceptMethod(Action method)
    {
        acceptMethod = method;
    }

    public void SetRefuseMethod(Action method)
    {
        refuseMethod = method;
    }

    public void SetDisabledCanvas(GameObject disabledCanvas)
    {
        this.disabledCanvas = disabledCanvas;
        this.disabledCanvas.SetActive(false);
    }

    public void Accept()
    {
        Destroy(gameObject);
        disabledCanvas.SetActive(true);
        if (acceptMethod != null) acceptMethod();
    }

    public void Refuse()
    {
        Destroy(gameObject);
        disabledCanvas.SetActive(true);
        if (refuseMethod != null) refuseMethod();
    }
}
