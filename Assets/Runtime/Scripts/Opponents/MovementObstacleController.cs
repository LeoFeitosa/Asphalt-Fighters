using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObstacleController : MonoBehaviour
{
    [SerializeField] float _maxSpeed;
    PlayerController _playerController;
    float _currentSpeed;
    Vector3 _position;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _position = transform.position;
        RandomX();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
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

    void RandomX()
    {
        float rand = 0;
        rand = Random.Range(-1.7f, 1.7f);
        _position.x = rand;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
