using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInteraction : MonoBehaviour
{
    [Header("Enter to the Game")]
    [SerializeField] private KeyCode _enterGame = KeyCode.Return;
    [SerializeField] private UnityEvent _enterGameEvent;

    [Space(1)]
    [Header("Return to Main Menu Game")]
    [SerializeField] private KeyCode _returnMainMenu = KeyCode.Escape;
    [SerializeField] private UnityEvent _returnMainMenuEvent;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_enterGame))
        {
            _enterGameEvent?.Invoke();
        }

        if(Input.GetKeyDown(_returnMainMenu))
        {
            _returnMainMenuEvent?.Invoke();
        }
    }
}
