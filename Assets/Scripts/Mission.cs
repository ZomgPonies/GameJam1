using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    public List<ObjectInteractive> m_objectListToPickup;
    public List<ObjectInteractive> m_objectListInScene;

    private int m_idCount;
    private List<ObjectInteractive> m_pickedUpObject;

    // Use this for initialization
    void Start()
    {
        m_idCount = 0;
        m_pickedUpObject = new List<ObjectInteractive>(m_objectListToPickup.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            ObjectInteractive myObj = m_objectListInScene[m_idCount];
            OnPickupObject(myObj);

            m_idCount++;
            if(m_idCount >= m_objectListInScene.Count)
            {
                m_idCount = 0;
            }
        }
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
