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
        m_mission.ChooseScenario(m_currentRoom);
        SoundManager.Instance.PlaySound(m_mission.GetIntroVoiceLine());
    }

    public void UpdateNextMission()
    {
        m_currentRoom = m_mission.GetNextMission();
        InitMission();
    }
}
