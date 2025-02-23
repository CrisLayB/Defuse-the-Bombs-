using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BombToSpawn
{
    public GameObject bombPrefab;
    public Transform pointToSpawn;
}

public class BombSpawner: MonoBehaviour
{    
    [SerializeField] private List<BombToSpawn> _bombsToSpawn;
    [SerializeField] private GameManager _gameManager;
    private bool[] _spawned;

    void Start()
    {        
        _spawned = new bool[_bombsToSpawn.Count];
        SetAllTheBombs();
    }

    private void SetAllTheBombs()
    {
        int bombCount = 0;
        while (bombCount < _bombsToSpawn.Count) 
        {
            int randomIndex = UnityEngine.Random.Range(0, _bombsToSpawn.Count);

            if (!_spawned[randomIndex])
            {
                BombToSpawn bombToSpawn = _bombsToSpawn[randomIndex];
                GameObject bomb = bombToSpawn.bombPrefab;
                Transform point = bombToSpawn.pointToSpawn;
                Instantiate(bomb, point.position,  point.rotation);
                _spawned[randomIndex] = true;
                bombCount += 1;
            }
        }

        _gameManager.SetTotalGames(bombCount);
    }
}