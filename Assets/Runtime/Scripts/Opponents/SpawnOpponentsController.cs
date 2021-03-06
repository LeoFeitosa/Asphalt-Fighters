using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnOpponentsController : MonoBehaviour
{
    [SerializeField] float _secondsToStartSpawn = 5;

    [Header("Prefabs cars and obstacles")]
    [SerializeField] GameObject[] _prefabCars;
    [SerializeField] GameObject[] _prefabObstacles;
    [SerializeField] Transform _spawnPoint;
    GameObject[] _opponents;
    [SerializeField] GameObject _finishComponent;

    [Header("Random Time To Spawn Cars")]
    [SerializeField] float _minSecondsToSpawn = 0.5f;
    [SerializeField] float _maxSecondsToSpawn = 3;

    GameManager _gameManager;
    PlayerCollidersController _player;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<PlayerCollidersController>();
    }

    void Start()
    {
        _opponents = _prefabCars.Concat(_prefabObstacles).ToArray();
        Invoke("StartSpawn", _secondsToStartSpawn);
    }

    void StartSpawn()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (!_gameManager.IsMoving)
        {
            _finishComponent.SetActive(true);
        }

        float seconds = Random.Range(_minSecondsToSpawn, _maxSecondsToSpawn);
        Instantiate(_opponents[Random.Range(0, _opponents.Length)], _spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);

        if (!_player.FinishedThePhase)
        {
            StartCoroutine(Spawn());
        }
    }
}
