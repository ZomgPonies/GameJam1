using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    public List<ObjectInteractive> m_objectList;  

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsObjectInMission(ObjectInteractive obj)
    {
        return m_objectList.Exists((ObjectInteractive intObj) => intObj.getID() == obj.getID());
    }
}
