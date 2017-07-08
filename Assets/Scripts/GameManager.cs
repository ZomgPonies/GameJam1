using UnityEngine;

public class GameManager: MonoBehaviour
{
    // Gravitie's strength
    public const float GRAVITY = 5.0f;

    private void Start()
    {
        CameraManager.Instance.ChangeCamera(CameraManager.FPS_CAMERA);
    }
}
