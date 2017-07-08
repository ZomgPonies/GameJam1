using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterInteraction))]

public class PlayerController : MonoBehaviour
{
    private CharacterMovement movementScript;
    private CharacterInteraction m_interactionScript;
    
    private void Awake()
    {
        movementScript = GetComponent<CharacterMovement>();
        m_interactionScript = GetComponent<CharacterInteraction>();
    }

    private void Update()
    {
        // For test purposess
        debugCommand();

        if (Time.deltaTime > 0)
        {
            Hashtable inputs = fetchInputs();
            
            // Move the character
            movementScript.MoveCharacter(inputs, true);
            
            // Move the camera
            if (CameraManager.Instance.CurrentCameraType != "")
            {
                switch (CameraManager.Instance.CurrentCameraType)
                {
                    case CameraManager.FPS_CAMERA:
                        CameraManager.Instance.CurrentCamera.GetComponent<FPSCameraControl>().RotateCameraWithInput(inputs, true);
                        break;
                }
            }

            // Interect with the environment and maybe try to select a gameobject
            m_interactionScript.Interact((bool)inputs["interactInput"]);
        }
    }

    // The Hashtable of inputs value must contain those keys:
    //-verticalInput
    //-horizontalInput
    //-xAxis
    //-yAxis
    Hashtable fetchInputs()
    {
        Hashtable inputs = new Hashtable();
        
        inputs.Add("verticalInput", Input.GetAxisRaw("Vertical"));
        inputs.Add("horizontalInput", Input.GetAxisRaw("Horizontal"));

        // The input of the joystick is prioritized
        if (Input.GetAxis("Right Stick X Axis") != 0)
        {
            inputs.Add("xAxis", Input.GetAxis("Right Stick X Axis"));
        }
        else
        {
            inputs.Add("xAxis", Input.GetAxis("Mouse X"));
        }

        if (Input.GetAxis("Right Stick Y Axis") != 0)
        {
            inputs.Add("yAxis", Input.GetAxis("Right Stick Y Axis"));
        }
        else
        {
            inputs.Add("yAxis", Input.GetAxis("Mouse Y"));
        }

        inputs.Add("interactInput", Input.GetButtonDown("Interact"));
        
        return inputs;
    }

    static void ClearConsole()
    {
        // This simply does "LogEntries.Clear()" the long way:
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    void debugCommand()
    {
        if (Input.GetButtonDown("Left Bumper"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0.2f;
            }
            else if (Time.timeScale == 0.2f)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetButtonDown("Back"))
        {
            Debug.ClearDeveloperConsole();
            SceneManager.LoadScene(0);
        }
    }
}
