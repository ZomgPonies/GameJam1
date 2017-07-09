using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    [SerializeField]
    private Button initiallySelectedButton;

    [SerializeField]
    private string introSceneName;

    private void Start()
    {
        initiallySelectedButton.Select();
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene(introSceneName);
    }
}