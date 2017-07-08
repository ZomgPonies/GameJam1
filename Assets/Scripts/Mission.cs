using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    public List<ObjectInteractive> m_objectListToPickup;
    
    private List<ObjectInteractive> m_pickedUpObject;

    // Use this for initialization
    void Start()
    {
        m_pickedUpObject = new List<ObjectInteractive>(m_objectListToPickup.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPickupObject(ObjectInteractive obj)
    {
        if (IsObjectInMission(obj))
        {
            obj.OnPickupGood();
            m_pickedUpObject.Add(obj);
            m_objectListToPickup.Remove(obj);

            if (m_objectListToPickup.Count <= 0)
            {
                Debug.Log("Good Job, you win the rite");
            }
        }
        else
        {
            obj.OnFailPickup();
        }
    }

    bool IsObjectInMission(ObjectInteractive obj)
    {
        return m_objectListToPickup.Exists((ObjectInteractive intObj) => intObj.getID() == obj.getID());
    }
}
