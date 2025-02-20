using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Mechanography : MonoBehaviour
{
    [SerializeField] private TMP_Text _sentenceTextbox;

    [Header("UnityEvents")]
    [SerializeField] private UnityEvent _winEvent;
    [SerializeField] private UnityEvent _returnEvent;
    [SerializeField] private UnityEvent _loseEvent;

    string[] _sentences = {"good morning", "nice weather", "i didn't do anything at all"};
    string _correctSentence;
    int _sentenceIndex = 0;
    int _characterIndex = 0;

    void Start()
    {
        _sentenceTextbox.text = _sentences[_sentenceIndex];
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            char input = Input.inputString[0];
            if (_sentences[_sentenceIndex][_characterIndex].Equals(input))
            {
                _correctSentence += input;
                string text = _sentences[_sentenceIndex];
                text = text.Remove(0,_correctSentence.Length).Insert(0, "<color=green>"+_correctSentence+"</color>");
                _sentenceTextbox.text = text;

                _characterIndex++;
                if (_characterIndex >= _sentences[_sentenceIndex].Length)
                {
                    _sentenceIndex++;
                    if (_sentenceIndex == _sentences.Length) {
                        _winEvent?.Invoke();
                        _returnEvent?.Invoke();
                        return;
                    }
                    _characterIndex = 0;
                    _correctSentence = "";
                    _sentenceTextbox.text = _sentences[_sentenceIndex];
                }
            } else {
                _sentenceTextbox.text = "<color=red>" + _sentenceTextbox.text + "</color>";
                _returnEvent?.Invoke();
            }
        }
    }
}