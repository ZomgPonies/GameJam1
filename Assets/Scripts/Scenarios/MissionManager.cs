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
        SoundManager.Instance.GetAudioSource().PlayOneShot(m_mission.GetIntroVoiceLine());
    }

    public void UpdateNextMission(int newRoomID)
    {
        m_currentRoom = newRoomID;
        InitMission();
    }
}
