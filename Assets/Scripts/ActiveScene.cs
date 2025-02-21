using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveScene : MonoBehaviour
{
    [SerializeField] private float _secondsShowAction = 3f;
    [SerializeField] private UnityEvent _actionToStart;
    [SerializeField] private GameObject _uiToShow;

    [SerializeField] private float _secondsToShowUI = 6f;
    
    private void OnEnable() 
    {
        StartCoroutine(ActionStart());   
    }

    private IEnumerator ActionStart()
    {        
        yield return new WaitForSeconds(_secondsShowAction);
        _actionToStart?.Invoke();
        yield return new WaitForSeconds(_secondsToShowUI);
        _uiToShow?.SetActive(true);
    }
}
