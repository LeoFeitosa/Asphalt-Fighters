using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePassageController : MonoBehaviour
{
    [Header("Position to close passage")]
    [SerializeField] float _closePassageY = -0.5f;
    [SerializeField] float _speed = 1.5f;

    bool _closePassage;
    bool _stopMove;
    Vector3 _target;
    bool _randomDirection;

    void Start()
    {
        _closePassage = false;
        _stopMove = false;
        _randomDirection = RandomDirection();
        _target = transform.position;
    }

    void Update()
    {
        ClosePassage();
        Move();
    }

    void ClosePassage()
    {
        if (!_closePassage && this.transform.position.y <= _closePassageY)
        {
            _closePassage = true;
        }
    }

    bool RandomDirection()
    {
        if (Random.value >= 0.5)
            return true; //right
        else
            return false; //left
    }

    void Move()
    {
        if (_closePassage)
        {
            _target.y = transform.position.y;

            if (!_stopMove)
            {
                if (_randomDirection)
                {
                    _target.x += _speed * Time.deltaTime;
                }
                else
                {
                    _target.x -= _speed * Time.deltaTime;
                }
            }

            transform.position = _target;
        }
        else
        {
            _target = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.LimitsClosePassage))
        {
            _stopMove = true;
        }
    }
}
