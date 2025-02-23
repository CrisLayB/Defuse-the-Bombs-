using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private Door _door = null;
    
    private Camera _playerCamera;

    private void Start() 
    {
        _playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update() 
    {
        if(_door == null) return;

        if(!Input.GetButtonDown("Interaction")) return;

        _door.InteracWithTheDoor();
    }

    private void FixedUpdate() 
    {
        if(!_playerCamera.enabled) return;

        Ray camRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        float maxRayDistance = 2f;

        if(Physics.Raycast(camRay, out hitInfo, maxRayDistance))
        {            
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.cyan);
            DetectDoor(hitInfo);
        }
        else
        {
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.blue);
            _door?.DeactivateTextEvent();
            _door = null;
        }
    }

    private void DetectDoor(RaycastHit hitInfo)
    {
        Door doorDetected = hitInfo.collider.gameObject.GetComponent<Door>();
        if(_door == null && doorDetected)
        {
            _door = doorDetected;
            _door.ActivateTextEvent();
        }
    }
}
