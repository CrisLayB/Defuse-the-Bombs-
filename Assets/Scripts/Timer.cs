using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

// References: https://www.youtube.com/watch?v=NJJK2ySgvXk&ab_channel=GameDevTraum 
public class Timer : MonoBehaviour
{
    // --> Atributos
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private TMP_Text _textTime;

    [Space(5)]
    [SerializeField] private UnityEvent _eventWhenTimeIsUp;
    
    private int _m, _s;
    private bool _timeup = false;

    private int _secondsState = 1;
    private bool _state = false;
    
    // --> Propiedades    
    public bool TimeUp
    {
        get { return _timeup; }
    }

    public bool State
    {
        get { return _state; }
    }

    // --> Métodos
    public void StartTimer()
    {
        _m = minutes;
        _s = seconds;
        WriteTimer(_m, _s);
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private void StopTimer()
    {
        CancelInvoke();
    }

    private void UpdateTimer()
    {          
        _secondsState++;        
        if(_secondsState > 10)
        {
            _state = !_state;
            _secondsState = 1;
        }
                  
        _s--;
        if(_s < 0)
        {
            if (_m == 0)
            {
                _timeup = true;
                _eventWhenTimeIsUp.Invoke();
                StopTimer();
            }
            else
            {
                _m--;
                _s = 59;
            }
        }
        WriteTimer(_m, _s);
    }

    private void WriteTimer(int _m, int _s)
    {
        _textTime.text = (_s < 10) ? _textTime.text = _m.ToString() + ":0" + _s.ToString() : _textTime.text= _m.ToString() + ":" + _s.ToString();
        if(_s <= 0) _textTime.text = _m.ToString() + ":00";
    }
}