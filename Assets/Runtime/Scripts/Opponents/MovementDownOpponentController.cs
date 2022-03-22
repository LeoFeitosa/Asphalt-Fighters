using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDownOpponentController : MonoBehaviour
{
    [SerializeField] float _speedDown = 5f;
    [SerializeField] int _timeToDestroy = 8;
    [SerializeField] bool enableMove = true;

    Vector3 _position;

    void Start()
    {
        Destroy(this.gameObject, _timeToDestroy);
        _position = transform.position;
        RandomX();
    }

    void Update()
    {
        MoveDown();
    }

    void MoveDown()
    {
        if (enableMove)
        {
            _position.y -= _speedDown * Time.deltaTime;
            transform.position = _position;
        }
    }

    void RandomX()
    {
        if (Random.value > 0.5)
        {
            _position.x = 1;
        }
        else
        {
            _position.x = -1;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
