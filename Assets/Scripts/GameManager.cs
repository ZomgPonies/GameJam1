using UnityEngine;
using System.Linq;

public class GameManager: MonoBehaviour
{
    // Gravitie's strength
    public const float GRAVITY = 5.0f;

    public const string PLAYER_TAG = "Player";
    
    private void Start()
    {
        // Activate FPS camera
        CameraManager.Instance.ChangeCamera(CameraManager.FPS_CAMERA);
        
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        player.GetComponent<PlayerController>().enabled = false;

        // Display loading title
        HUDManager.Instance.ChangeDisplayedHUD(HUDManager.Instance.loadingCanvas);

        LoadData();

        // Fade out the loading HUD after all the loading and launch the game after the fade out
        HUDManager.Instance.FadeOutHud(HUDManager.Instance.loadingCanvas, StartGame);
    }

    private void LoadData()
    {
        ScenarioLoader.GetInstance().LoadScenarios();
        InteractiveObjectModelLoader.GetInstance().LoadInteractiveObjectModels();

        // Give each interactive object the correct model
        foreach (InteractiveObjectModel interactiveObjectModel in InteractiveObjectModelLoader.GetInstance().interactiveObjectModels)
        {
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == interactiveObjectModel.gameObjectName);

            foreach(var obj in objects)
            {
                if (obj != null)
                {
                    InteractiveObject interactiveObjectScript = obj.GetComponent<InteractiveObject>();

                    if (interactiveObjectScript != null)
                    {
                        interactiveObjectScript.SetModel(interactiveObjectModel);
                    }
                    else
                    {
                        Debug.LogError(interactiveObjectModel.gameObjectName + " GameObject doesn't have an InteractiveObject script");
                    }
                }
                else
                {
                    Debug.LogError(interactiveObjectModel.gameObjectName + " GameObject wasn't found");
                }
            }
        }
    }

    public void StartGame()
    {
        HUDManager.Instance.FadeInHud(HUDManager.Instance.HUDInventory);

        Screen.lockCursor = true;

        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        player.GetComponent<PlayerController>().enabled = true;
        
        // Init the first mission with the right scenario
        MissionManager.Instance.InitMission();

        Debug.Log("Start a game");
    }
}
