using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public InteractiveObjectModel m_model { get; private set; }

    [SerializeField]
    private AudioClip validSound;
    [SerializeField]
    private AudioClip invalidSound;

    // Use this for initialization
    void Start()
    {
    }

    public int GetID()
    {
        return m_model.id;
    }

    public void SetModel(InteractiveObjectModel model)
    {
        m_model = model;
    }

    public void OnFailPickup()
    {
        if (invalidSound != null) SoundManager.Instance.GetAudioSource().PlayOneShot(invalidSound);
        Debug.Log("MMmhh. The " + name + " object is not the right item to pick up.");
    }

    public void OnPickupGood()
    {
        if (validSound != null) SoundManager.Instance.GetAudioSource().PlayOneShot(validSound);
        Debug.Log("Good job, you succesly pick up the " + name + " object.");
    }
}
