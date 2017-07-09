using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCanvas : MonoBehaviour
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

        Screen.lockCursor = false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadCredit(Button clickedButton)
    {
        Canvas creditCanvas = Instantiate(this.creditCanvas);
        creditCanvas.GetComponent<CreditCanvas>().SetCallBackMethodOnClose(clickedButton.Select);
        creditCanvas.GetComponent<CreditCanvas>().SetDisabledCanvas(gameObject);
    }

    public void LoadHelp(Button clickedButton)
    {
        Canvas helpCanvas = Instantiate(this.helpCanvas);
        helpCanvas.GetComponent<HelpCanvas>().SetCallBackMethodOnClose(clickedButton.Select);
        helpCanvas.GetComponent<HelpCanvas>().SetDisabledCanvas(gameObject);
    }
}
