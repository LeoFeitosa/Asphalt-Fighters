using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{
    [SerializeField] Transform _target;
    GameManager _gameManager;
    Transform _startPosition;
    float _speed;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        _startPosition = transform;
        _speed = Vector3.Distance(_target.position, _startPosition.position) / _gameManager.LevelDuration;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(_startPosition.position, _target.position, _speed * Time.deltaTime);
    }
}
