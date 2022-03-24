using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFixedController : MonoBehaviour
{
    [SerializeField] bool _fixedPosition;
    PlayerController _playerController;
    float _currentSpeed;
    Vector3 _position;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
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
        _currentSpeed = _playerController.Velocity;

        _position.y -= _currentSpeed * Time.deltaTime;

        this.transform.position = _position;
    }

    void RandomX()
    {
        if (!_fixedPosition)
        {
            float rand = 0;
            rand = Random.Range(-1.7f, 1.7f);
            _position.x = rand;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
