using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerMouvement script requires thoses components and will be added if they aren't already there
[RequireComponent(typeof(CharacterController))]

public class CharacterInteraction : MonoBehaviour
{
    public bool m_debugRaycast;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Interact()
    {
        GameObject camera = CameraManager.Instance.CurrentCamera;

        InteractiveObject myObj = getInteractiveObject(camera.transform.position, camera.transform.forward);

        if (myObj == null)
        {
            return;
        }

        Debug.Log("We got an interactive object (" + myObj.m_model.name + ")");

        // TODO - Savoir si cet objet est bien celui de la mission
    }

    InteractiveObject getInteractiveObject(Vector3 origin, Vector3 direction)
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
    }

    GameObject getObject(Vector3 origin, Vector3 direction)
    {
        RaycastHit myHit;

        if (m_debugRaycast)
        {
            Debug.DrawRay(origin, direction, Color.blue, 5.0f);
        }

        if (Physics.Raycast(origin, direction, out myHit))
        {
            return myHit.collider.gameObject;
        }

        return null;
    }
}
