using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombGameEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] gameElements;
    
    [SerializeField] private UnityEvent activateTextEvent;
    [SerializeField] private UnityEvent deactivateTextEvent;
    [SerializeField] private float secondsToReturn = 5f;
    [SerializeField] private UnityEvent _eventAfterReturn;

    private bool _isSuccess = false;
    private bool _isFinished = false;

    public bool IsSucess() => _isSuccess;

    public bool IsFinished() => _isFinished;

    private void Awake() 
    {                                
        if(gameElements.Length == 0) Debug.Log("Game Elements List is empty");
        
        if(gameElements == null) Debug.LogError("GameCamera is null");
        
        ToogleObjectsOfGame(false);

        deactivateTextEvent?.Invoke();
    }

    public void GameEventWinned() => _isSuccess = true;

    public void ActiveGametext() 
    {
        if(_isSuccess) return;
        
        activateTextEvent?.Invoke();
    }
    public void DeactiveGametext() => deactivateTextEvent?.Invoke();

    public void EnterToTheGame()
    {
        ToogleObjectsOfGame(true);
    }

    public void ReturnToMainGame()
    {
        StartCoroutine(ReturnToTheGameCoroutine());
    }

    private IEnumerator ReturnToTheGameCoroutine()
    {
        _isFinished = true;
        yield return new WaitForSeconds(secondsToReturn);
        _eventAfterReturn?.Invoke();
    }

    private void ToogleObjectsOfGame(bool toogle)
    {
        foreach (GameObject element in gameElements)
        {
            element.SetActive(toogle);
        }
    }
}
