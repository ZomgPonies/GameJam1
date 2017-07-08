using UnityEngine;

public class MissionManager : MonoSingleton<MissionManager>
{
    private int m_currentRoom;
    public Mission m_mission { get; private set; }

    void Awake()
    {
        m_currentRoom = 0;
        m_mission = GetComponent<Mission>();
    }
    
    public void InitMission()
    {
        Debug.Log(m_mission);
        m_mission.ChooseScenario(m_currentRoom);
    }

    public void UpdateNextMission(int newRoomID)
    {
        m_currentRoom = newRoomID;
        InitMission();
    }
}
