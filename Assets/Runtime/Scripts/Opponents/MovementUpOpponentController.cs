using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUpOpponentController : MonoBehaviour
{
    [SerializeField] float _speedUp = 3f;
    [SerializeField] int _timeToDestroy = 8;

    Vector3 _position;

    void Start()
    {
        Destroy(this.gameObject, _timeToDestroy);
        _position = transform.position;
    }

    void Update()
    {
        MoveUp();
    }

    void MoveUp()
    {
        _position.y += _speedUp * Time.deltaTime;
        this.transform.position = _position;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
