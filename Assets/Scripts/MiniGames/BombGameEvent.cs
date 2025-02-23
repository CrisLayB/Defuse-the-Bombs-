using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombGameEvent : MonoBehaviour
{
    [Header("When Game Its activated")]
    [SerializeField] private GameObject[] gameElementsToActivateForPlay;
    [SerializeField] private UnityEvent _optionalExtraEffects;

    [Space(1)]
    [Header("Propierties when the game is finished")]
    [SerializeField] private float secondsToReturn = 5f;
    [SerializeField] private UnityEvent _eventAfterReturn;

    // Escential Properties to Get
    private BombInteraction _playerInteraction;
    private GameObject _uiActiveText;
    private GameManager _gameManager;

    private bool _isSuccess = false;
    private bool _isFinished = false;

    public bool IsSucess() => _isSuccess;

    public bool IsFinished() => _isFinished;

    private void Start() 
    {                                
        _playerInteraction = FindObjectOfType<BombInteraction>();
        _uiActiveText = GameObject.Find("Canvas")?.transform.Find("DeactiveBombImage")?.gameObject;
        _gameManager = FindObjectOfType<GameManager>();

        if(_playerInteraction == null) Debug.LogError("PlayerInteraction is null");

        if(_uiActiveText == null) Debug.LogError("UI Active Text is null");

        if(_gameManager == null) Debug.LogError("GameManager is null");
        
        if(gameElementsToActivateForPlay.Length == 0) Debug.Log("Game Elements List is empty");
        
        if(gameElementsToActivateForPlay == null) Debug.LogError("GameCamera is null");
        
        ToogleObjectsOfGame(false);
        
        DeactiveGametext();
    }

    public void GameEventWinned()
    {
        _isSuccess = true;
        _gameManager.OnePoinWinned();
    }

    public void ActiveGametext() 
    {
        if(_isSuccess) return;
        
        _uiActiveText?.SetActive(true);
    }
    public void DeactiveGametext()
    {
        _uiActiveText?.SetActive(false);
    }

    public void EnterToTheGame()
    {
        DeactiveGametext();
        ToogleObjectsOfGame(true);
        _optionalExtraEffects?.Invoke();
    }

    public void ReturnToMainGame()
    {
        StartCoroutine(ReturnToTheGameCoroutine());
    }

    private IEnumerator ReturnToTheGameCoroutine()
    {
        _isFinished = true;
        yield return new WaitForSeconds(secondsToReturn);
        _playerInteraction.ActiveThePlayer();
        _eventAfterReturn?.Invoke();
    }

    private void ToogleObjectsOfGame(bool toogle)
    {
        foreach (GameObject element in gameElementsToActivateForPlay)
        {
            element.SetActive(toogle);
        }
    }
}
