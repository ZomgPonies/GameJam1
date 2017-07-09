using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private Button initiallySelectedButton;

    [SerializeField]
    private string gameSceneName;

    [SerializeField]
    private Canvas creditCanvas;

    [SerializeField]
    private Canvas helpCanvas;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadCredit(Button clickedButton)
    {
        Canvas creditCanvas = Instantiate(this.creditCanvas);
        creditCanvas.GetComponent<CreditManager>().SetCallBackMethodOnClose(clickedButton.Select);
        creditCanvas.GetComponent<CreditManager>().SetDisabledCanvas(gameObject);
    }

    public void LoadHelp(Button clickedButton)
    {
        Canvas helpCanvas = Instantiate(this.helpCanvas);
        helpCanvas.GetComponent<HelpManager>().SetCallBackMethodOnClose(clickedButton.Select);
        helpCanvas.GetComponent<HelpManager>().SetDisabledCanvas(gameObject);
    }
}
