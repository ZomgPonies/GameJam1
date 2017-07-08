using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public bool m_debugRaycast;

    [SerializeField]
    private LayerMask interactiveObjectLayer;

    [SerializeField]
    private float range = 1.0f;
    private GameObject currentInteractedObjectGameObject;
    
    public void Interact(bool selected)
    {
        GameObject camera = CameraManager.Instance.CurrentCamera;

        GameObject myObj = getObject(camera.transform.position, camera.transform.forward);

        if (currentInteractedObjectGameObject != null && currentInteractedObjectGameObject != myObj) currentInteractedObjectGameObject.GetComponent<GlowObject>().StopGlow();

        if (myObj != null)
        {
            currentInteractedObjectGameObject = myObj;
            currentInteractedObjectGameObject.GetComponent<GlowObject>().TriggerGlow();

            if (selected) MissionManager.Instance.m_mission.OnPickupObject(myObj.GetComponent<InteractiveObject>());
        }
    }

    /*InteractiveObject getInteractiveObject(Vector3 origin, Vector3 direction)
    {
        GameObject obj = getObject(origin, direction);
        if (obj)
        {
            InteractiveObject intObj = obj.GetComponent<InteractiveObject>();
            if(intObj)
            {
                return intObj;
            }
        }

        return null;
    }*/

    GameObject getObject(Vector3 origin, Vector3 direction)
    {
        RaycastHit myHit;

        if (m_debugRaycast)
        {
            Debug.DrawRay(origin, direction, Color.blue, 5.0f);
        }

        if (Physics.Raycast(origin, direction, out myHit, range, interactiveObjectLayer))
        {
            return myHit.collider.gameObject;
        }

        return null;
    }
}
