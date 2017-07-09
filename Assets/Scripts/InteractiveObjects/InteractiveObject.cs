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
        if (invalidSound != null)
        {
            SoundManager.Instance.GetAudioSource().PlayOneShot(invalidSound);
        }
        GetComponent<GlowObject>().FailPickup();
    }

    public void OnPickupGood()
    {
        if (validSound != null)
        {
            SoundManager.Instance.GetAudioSource().PlayOneShot(validSound);
        }
        Debug.Log("Good job, you successly pick up the " + name + " object.");
    }
}
