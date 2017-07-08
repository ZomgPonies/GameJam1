using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private InteractiveObjectModel m_model;

    // Use this for initialization
    void Start()
    {
    }

    public int getID()
    {
        return m_model.id;
    }

    public void setModel(InteractiveObjectModel model)
    {
        m_model = model;
    }

    public void OnFailPickup()
    {
        Debug.Log("MMmhh. The " + name + " object is not the right item to pick up.");
    }

    public void OnPickupGood()
    {
        Debug.Log("Good job, you succesly pick up the " + name + " object.");
    }
}
