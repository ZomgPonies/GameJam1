using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractive : MonoBehaviour
{

    public ObjectIdentificator m_ObjectIdentificator;

    private int m_ID;

    // Use this for initialization
    void Start()
    {
        m_ID = m_ObjectIdentificator.getNextObjectID();
        Debug.Log(name + " is with ID : " + m_ID);
    }

    public int getID()
    {
        return m_ID;
    }

    public void OnFailPickup()
    {

    }

    public void OnPickupGood()
    {

    }
}
