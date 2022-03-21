using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float _maxSpeed;
    [SerializeField] Object _prefabRoad;

    PlayerController _playerController;
    float _currentSpeed;
    bool _permissionToInstantiate;
    int _numberOfChildObjects;
    int _fullSize;
    Vector3 _position;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _numberOfChildObjects = GetComponentsInChildren<SpriteRenderer>().Length;
        _fullSize = (int)GetComponentInChildren<SpriteRenderer>().bounds.size.y;
    }

    void Start()
    {
        _position = transform.position;
        _currentSpeed = _playerController.Velocity;
    }

    void Update()
    {
        EnableInstantiate();
        MoveHorizontal();
        InstantiateNewRoad();
        DestroyRoad();
    }

    void MoveHorizontal()
    {
        if (_currentSpeed < _maxSpeed)
        {
            _currentSpeed = _playerController.Velocity;
        }
        else
        {
            _currentSpeed = _maxSpeed;
        }

        _position.y -= _currentSpeed * Time.deltaTime;

        this.transform.position = _position;
    }

    void InstantiateNewRoad()
    {
        if ((int)Mathf.Abs(this.transform.position.y) == _fullSize && _permissionToInstantiate)
        {
            DisableInstantiate();
            Instantiate(_prefabRoad, new Vector3(0, this.transform.position.y + _fullSize * _numberOfChildObjects, 0), Quaternion.identity);
            return;
        }
    }

    void DisableInstantiate()
    {
        _permissionToInstantiate = false;
    }

    void EnableInstantiate()
    {
        if ((int)Mathf.Abs(this.transform.position.y) == 0)
        {
            _permissionToInstantiate = true;
        }
    }

    void DestroyRoad()
    {
        if ((int)Mathf.Abs(this.transform.position.y) > _fullSize * 2)
        {
            Destroy(this.gameObject);
        }
    }
}
