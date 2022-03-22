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
        float rand = 0;

        if (Random.value > 0.5)
        {
            rand = Random.Range(-1.7f, -0.32f);
        }
        else
        {
            rand = Random.Range(0.32f, 1.7f);
        }

        _position.x = rand;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
