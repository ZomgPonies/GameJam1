using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
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

        confirmCanvasGameObject.GetComponent<ConfirmCanvas>().SetAcceptMethod(Application.Quit);
        confirmCanvasGameObject.GetComponent<ConfirmCanvas>().SetRefuseMethod(clickedButton.Select);
        confirmCanvasGameObject.GetComponent<ConfirmCanvas>().SetDisabledCanvas(gameObject);
    }

    public void LoadHelp(Button clickedButton)
    {
        Canvas helpCanvasGameObject = Instantiate(helpCanvas);
        helpCanvasGameObject.GetComponent<HelpCanvas>().SetCallBackMethodOnClose(clickedButton.Select);
        helpCanvasGameObject.GetComponent<HelpCanvas>().SetDisabledCanvas(gameObject);
    }

    public void LoadIntro()
    {
        if (callBackMethod != null) callBackMethod();
        SceneManager.LoadScene(introSceneName);
    }
}
