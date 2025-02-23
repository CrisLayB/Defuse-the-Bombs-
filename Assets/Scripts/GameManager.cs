using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _beforeShowWinEvent;
    [SerializeField] private UnityEvent _winEvent;
    private int counterTotalGames = 0;
    private int counterWinnedGames = 0;
    private bool _finished = false;

    public void SetTotalGames(int totalGames)
    {
        counterTotalGames = totalGames;
    }

    public void OnePoinWinned()
    {            
        counterWinnedGames++;

        if(counterWinnedGames >= counterTotalGames)
        {
            StartCoroutine(ThrowGameWinnedCutscene());
            _finished = true;
        }
    }

    private IEnumerator ThrowGameWinnedCutscene()
    {
        _beforeShowWinEvent?.Invoke();
        yield return new WaitForSeconds(3f);
        _winEvent?.Invoke();
    }
}
