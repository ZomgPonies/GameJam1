using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Door : MonoBehaviour
{
    public const string OPEN_PARAM_NAME_STRING = "Open";
    public const string CLOSE_PARAM_NAME_STRING = "Close";

    private Animator animator;

    private int openParamHashId;
    private int closeParamHashId;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        openParamHashId = Animator.StringToHash(OPEN_PARAM_NAME_STRING);
        closeParamHashId = Animator.StringToHash(CLOSE_PARAM_NAME_STRING);

        IsOpen = false;
    }

    public void OpenDoor()
    {
        animator.SetTrigger(openParamHashId);
    }

    public void CloseDoor()
    {
        animator.SetTrigger(closeParamHashId);
    }

    public void SetOpenState()
    {
        IsOpen = true;
    }

    public void SetCloseState()
    {
        IsOpen = false;
    }
}
