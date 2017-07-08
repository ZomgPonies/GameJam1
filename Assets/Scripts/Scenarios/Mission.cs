using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    private Scenario m_scenario;    
    private List<InteractiveObject> m_pickedUpObject;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChooseScenario(int roomID)
    {
        Scenario[] roomScenarios = ScenarioLoader.GetInstance().GetByRoomId(roomID);
        m_scenario = roomScenarios[Random.Range(0, roomScenarios.Length - 1)];
        m_pickedUpObject = new List<InteractiveObject>(m_scenario.objects.Length);

        Debug.Log("You choose the " + m_scenario.name + " scenario");
    }

    public void OnPickupObject(InteractiveObject obj)
    {
        if (IsObjectInMission(obj))
        {
            obj.OnPickupGood();
            m_pickedUpObject.Add(obj);

            if (m_pickedUpObject.Count >= m_pickedUpObject.Capacity)
            {
                Debug.Log("Good Job, you win the rite");
            }
        }
        else
        {
            obj.OnFailPickup();
        }
    }

    bool IsObjectInMission(InteractiveObject obj)
    {
        foreach(int id in m_scenario.objects)
        {
            if(id == obj.GetID())
            {
                return true;
            }
        }
        return false;
    }
}
