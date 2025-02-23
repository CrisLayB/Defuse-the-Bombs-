using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Mechanography : MonoBehaviour
{
    [SerializeField] private TMP_Text _sentenceTextbox;

    [Header("UnityEvents")]
    [SerializeField] private UnityEvent _winEvent;
    [SerializeField] private UnityEvent _returnEvent;
    [SerializeField] private UnityEvent _loseEvent;

    private List<string> _sentences = new List<string> {
        "good morning", 
        "nice weather", 
        "i didn't do anything", 
        "all your base belong to us", 
        "nothing can go wrong"
        };
    private string _currentSentence;
    private string _correctSentence;
    private int _sentenceCount = 0;
    private int _characterIndex = 0;

    void Start()
    {
        _currentSentence = GetSentence();
        _sentenceTextbox.text = _currentSentence;
    }

    void Update()
    {
        if (Input.anyKeyDown && _sentenceCount < 3) {
            char input = Input.inputString[0];
            if (_currentSentence[_characterIndex].Equals(input))
            {
                _correctSentence += input;
                string text = _currentSentence;
                text = text.Remove(0,_correctSentence.Length).Insert(0, "<color=green>"+_correctSentence+"</color>");
                _sentenceTextbox.text = text;

                _characterIndex++;
                if (_characterIndex >= _currentSentence.Length)
                {
                    _currentSentence = GetSentence();
                    _sentenceCount++;
                    if (_sentenceCount == 3) {
                        _winEvent?.Invoke();
                        _returnEvent?.Invoke();
                        return;
                    }
                    _characterIndex = 0;
                    _correctSentence = "";
                    _sentenceTextbox.text = _currentSentence;
                }
            } else {
                _sentenceTextbox.text = "<color=red>" + _currentSentence + "</color>";
                _returnEvent?.Invoke();
            }
        }
    }

    private string GetSentence()
    {
        int i = Random.Range(0, _sentences.Count);
        string sentence = _sentences[i];
        _sentences.RemoveAt(i);
        return sentence;
    }
}