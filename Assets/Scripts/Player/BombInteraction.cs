using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInteraction : MonoBehaviour
{
    private BombGameEvent _bombGameEvent = null;
    
    private FirstPersonMovement _movement;
    private Jump _jump;
    private Crouch _crouch;
    private Camera _playerCamera;
    
    private void Start()
    {
        _movement = GetComponent<FirstPersonMovement>();
        _jump = GetComponent<Jump>();
        _crouch = GetComponent<Crouch>();

        _playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update() 
    {
        if(_bombGameEvent == null) return;
        
        if(Input.GetButtonDown("Interaction") && !_bombGameEvent.IsSucess()) // Allow press the button for interaction
        {
            TogglePlayerControl(false);
            _bombGameEvent.EnterToTheGame();
        }
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
            DetectBomb(hitInfo);
        }
        else
        {
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.blue);
            _bombGameEvent?.DeactiveGametext();
            _bombGameEvent = null;       
        }
    }    

    public void ActiveThePlayer()
    {
        TogglePlayerControl(true);
    }

    private void DetectBomb(RaycastHit hitInfo)
    {
        BombGameEvent bombDetected = hitInfo.collider.gameObject.GetComponent<BombGameEvent>();
        if(_bombGameEvent == null && bombDetected)
        {
            _bombGameEvent = bombDetected;
            _bombGameEvent.ActiveGametext();
        }
    }

    private void TogglePlayerControl(bool isActive)
    {
        if (_movement != null) _movement.enabled = isActive;
        if (_jump != null) _jump.enabled = isActive;
        if (_crouch != null) _crouch.enabled = isActive;
        if (_playerCamera != null) _playerCamera.enabled = isActive;

        // TODO: CHECK CONDITION:
    }
}
