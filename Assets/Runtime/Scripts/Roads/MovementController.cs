using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    Object _prefabRoad;

    [SerializeField]
    GameObject _scenario;
    GameObject _instancedScenario;

    PlayerController _playerController;
    PlayerCollidersController _playerCollider;
    float _currentSpeed;
    bool _permissionToInstantiate;
    int _numberOfChildObjects;
    int _fullSize;
    Vector3 _position;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerCollider = FindObjectOfType<PlayerCollidersController>();
        _numberOfChildObjects = GetComponentsInChildren<SpriteRenderer>().Length;
        _fullSize = (int)GetComponentInChildren<SpriteRenderer>().bounds.size.y;
    }

    void Start()
    {
        _position = transform.position;
        _currentSpeed = _playerController.Velocity;
        _instancedScenario = Instantiate(
            _scenario,
            new Vector3(0, this.transform.position.y + _fullSize * _numberOfChildObjects, 0),
            Quaternion.identity
        );
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
        if (!_playerCollider.FinishedThePhase)
        {
            _currentSpeed = _playerController.Velocity;
            _position.y -= _currentSpeed * Time.deltaTime;
            this.transform.position = _position;
            _instancedScenario.transform.position = _position;
        }
    }

    void InstantiateNewRoad()
    {
        if (
            (int)Mathf.Abs(this.transform.position.y) == _fullSize
            && _permissionToInstantiate
            && !_playerCollider.FinishedThePhase
        )
        {
            DisableInstantiate();
            Instantiate(
                _prefabRoad,
                new Vector3(0, this.transform.position.y + _fullSize * _numberOfChildObjects, 0),
                Quaternion.identity
            );
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
            Destroy(_instancedScenario);
        }
    }
}
