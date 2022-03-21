using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Object _prefabRoad;

    bool _permissionToInstantiate;
    int _numberOfChildObjects;
    int _fullSize;
    Vector3 _position;

    void Awake()
    {
        _numberOfChildObjects = GetComponentsInChildren<SpriteRenderer>().Length;
        _fullSize = (int)GetComponentInChildren<SpriteRenderer>().bounds.size.y;
    }

    void Start()
    {
        _position = transform.position;
    }

    void FixedUpdate()
    {
        EnableInstantiate();
        MoveHorizontal();
        InstantiateNewRoad();
        DestroyRoad();
    }

    void MoveHorizontal()
    {
        _position.y -= _speed * Time.fixedDeltaTime;
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
