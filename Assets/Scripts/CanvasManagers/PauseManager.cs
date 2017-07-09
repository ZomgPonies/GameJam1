using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private Button initiallySelectedButton;

    private Action callBackMethod;

    [SerializeField]
    private Canvas confirmCanvas;

    [SerializeField]
    private Canvas helpCanvas;

    [SerializeField]
    private string introSceneName;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void SetCallBackMethodOnClose(Action method)
    {
        callBackMethod = method;
    }

    public void ClosePause()
    {
        Destroy(gameObject);
        if (callBackMethod != null) callBackMethod();
    }

    public void QuitGame(Button clickedButton)
    {
        Canvas confirmCanvasGameObject = Instantiate(confirmCanvas);

        confirmCanvasGameObject.GetComponent<ConfirmManager>().SetAcceptMethod(Application.Quit);
        confirmCanvasGameObject.GetComponent<ConfirmManager>().SetRefuseMethod(clickedButton.Select);
        confirmCanvasGameObject.GetComponent<ConfirmManager>().SetDisabledCanvas(gameObject);
    }

    public void LoadHelp(Button clickedButton)
    {
        Canvas helpCanvasGameObject = Instantiate(helpCanvas);
        helpCanvasGameObject.GetComponent<HelpManager>().SetCallBackMethodOnClose(clickedButton.Select);
        helpCanvasGameObject.GetComponent<HelpManager>().SetDisabledCanvas(gameObject);
    }

    public void LoadIntro()
    {
        if (callBackMethod != null) callBackMethod();
        SceneManager.LoadScene(introSceneName);
    }
}
