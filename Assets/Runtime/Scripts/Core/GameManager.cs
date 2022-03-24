using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _levelDuration;
    float _countMovementDuration;

    public bool IsMoving { get; private set; }
    public float LevelDuration { get; private set; }

    void Start()
    {
        LevelDuration = _levelDuration;
        _countMovementDuration = 1f;
    }

    void Update()
    {
        TimeToFinish();
    }

    void TimeToFinish()
    {
        if (_countMovementDuration <= _levelDuration)
        {
            _countMovementDuration += Time.deltaTime;
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }
}
