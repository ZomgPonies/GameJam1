using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractive : MonoBehaviour
{

    private int m_ID;

    // Use this for initialization
    void Start()
    {
        m_ID = ObjectIdentificator.GetInstance().getNextObjectID();
        Debug.Log(name + " is with ID : " + m_ID);
    }

    public int getID()
    {
        return m_ID;
    }

    public void setID(int ID)
    {
        m_ID = ID;
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
