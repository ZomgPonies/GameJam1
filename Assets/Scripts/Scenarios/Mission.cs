using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public Canvas m_endingScreen;
    public List<AudioClip> m_initSound = new List<AudioClip>();

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

        if(roomScenarios.Length <= 0)
        {
            Canvas ending = Instantiate(m_endingScreen);
            ending.GetComponent<ScenarioCanvas>().SetCallBackMethodOnClose(
            () =>
            {
                Application.LoadLevel("intro");
            });
            return;
        }

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
                GameObject room = GameObject.Find("Room_" + m_scenario.associatedRoomId);
                if(room)
                {
                    Door door = room.GetComponentInChildren<Door>();
                    if (door)
                    {
                        door.OpenDoor();
                    }
                    else
                    {
                        MissionManager.Instance.UpdateNextMission();
                    }
                }
            }
        }
        else
        {
            obj.OnFailPickup();
        }
    }

    bool IsObjectInMission(InteractiveObject obj)
    {
        if(AlreadyPickup(obj))
        {
            return false;
        }

        foreach(int id in m_scenario.objects)
        {
            if(id == obj.GetID())
            {
                return true;
            }
        }
        return false;
    }

    bool AlreadyPickup(InteractiveObject obj)
    {
        return m_pickedUpObject.Find((InteractiveObject o) => o.name == obj.name);
    }

    public AudioClip GetIntroVoiceLine()
    {
        return m_initSound.Find((AudioClip audio) => audio.name == m_scenario.soundFileName);
    }
    public int GetNextMission()
    {
        return m_scenario.nextMission;
    }
}
