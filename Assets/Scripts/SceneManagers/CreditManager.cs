using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    [SerializeField]
    private string introSceneName;

    private void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene(introSceneName);
    }
}
