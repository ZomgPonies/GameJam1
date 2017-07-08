using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoSingleton<MissionManager>
{
    private int m_currentRoom;
    public Mission m_mission { get; private set; }

    // Use this for initialization
    void Start()
    {
        m_currentRoom = 0;
        m_mission = GetComponent<Mission>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitMission()
    {
        m_mission.ChooseScenario(m_currentRoom);
    }

    public void UpdateNextMission(int newRoomID)
    {
        m_currentRoom = newRoomID;
        InitMission();
    }
}
