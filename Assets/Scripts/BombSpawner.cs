using UnityEngine;

public class BombSpawner: MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private int _bombAmount = 2;
    private Transform[] _spawnPoints;
    private bool[] _spawned;

    void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();
        _spawned = new bool[_spawnPoints.Length];
        if (_spawnPoints.Length <= _bombAmount)
            Debug.LogError("The amount of bombs is greater than the spawnpoints available");

        int bombCount = 0;
        while (bombCount < _bombAmount) {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            if (!_spawned[randomIndex])
            {
                Instantiate(_bombPrefab, _spawnPoints[randomIndex].position,  _spawnPoints[randomIndex].rotation);
                _spawned[randomIndex] = true;
                bombCount += 1;
            }
        }
        
    }
}