using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isDoorClosed = true;
    private GameObject _uiActiveText;
    
    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _uiActiveText = GameObject.Find("Canvas")?.transform.Find("TextDoor")?.gameObject;

        if(_uiActiveText == null) Debug.LogError("No active text for door founded");
    }
    
    public void InteracWithTheDoor()
    {
        _isDoorClosed = !_isDoorClosed;
        _animator.SetBool("isClosed", _isDoorClosed);
    }

    public void ActivateTextEvent()
    {
        _uiActiveText.SetActive(true);
    }

    public void DeactivateTextEvent()
    {
        _uiActiveText.SetActive(false);
    }
}
