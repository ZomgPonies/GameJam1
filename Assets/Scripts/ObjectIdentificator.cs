using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIdentificator : Singleton<ObjectIdentificator>
{

    static private int m_ObjectID;

    // Use this for initialization
    void Start()
    {
        m_ObjectID = 0;
    }

    public int getNextObjectID()
    {
        return m_ObjectID++;
    }

}
