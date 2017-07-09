using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorTrigger : MonoBehaviour
{
    public Door m_doorToClose;
    public Canvas m_outroCanvas;
    public AudioClip m_endingSound;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        m_doorToClose.CloseDoor();

        Canvas outroCanvas = Instantiate(m_outroCanvas);
        SoundManager.Instance.PlaySound(m_endingSound);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;

        outroCanvas.GetComponent<ScenarioCanvas>().SetCallBackMethodOnClose(
            () =>
            {
                MissionManager.Instance.UpdateNextMission();
                player.GetComponent<PlayerController>().enabled = true;
            });
    }
}
