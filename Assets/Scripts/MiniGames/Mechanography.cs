using UnityEngine;

public class Mechanography : MonoBehaviour
{
    string[] _sentences = {"good morning", "nice weather", "i didn't do anything at all"};
    int _sentenceIndex = 0;
    int _characterIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            char input = Input.inputString[0];
            if (_sentences[_sentenceIndex][_characterIndex].Equals(input))
            {
                Debug.Log("Key: " + input);
                _characterIndex += 1;
                if (_characterIndex >= _sentences[_sentenceIndex].Length)
                {
                    _sentenceIndex += 1;
                    _characterIndex = 0;
                }
            } else {
                Debug.Log("Failed");
            }
        }
    }
}