using NUnit.Framework.Constraints;
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
    private string creditSceneName;

    [SerializeField]
    private string helpSceneName;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene(creditSceneName);
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene(helpSceneName);
    }
}
