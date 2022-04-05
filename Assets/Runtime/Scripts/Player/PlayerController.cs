using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerController : MonoBehaviour
{
    PlayerInputController _input;
    PlayerCollidersController _gameCollider;
    PlayerAnimatorController _animatorController;
    Rigidbody2D _rb2D;
    Vector3 _moveDirections;
    float _currentSpeed;
    float _currentSpeedMove;
    bool _isMoveAfterCollision;
    float _randomDirectionAfterCollision;

    [Header("No Collision")]
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _acceleration = 0.002f;

    [Header("After Collision")]
    [SerializeField] float _maximumSpeedAfterCollision;
    [SerializeField] float _maximumSpeedAfterDoubleCollision;
    [SerializeField] float _percentageSpeedMoveAfterCollision;
    [SerializeField] float _percentageSpeedMoveAfterDoubleCollision;

    public float Velocity { get; private set; }

    void Awake()
    {
        _input = GetComponent<PlayerInputController>();
        _gameCollider = FindObjectOfType<PlayerCollidersController>();
        _animatorController = GetComponent<PlayerAnimatorController>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _currentSpeed = _maxSpeed;
        _currentSpeedMove = _speed;
        _isMoveAfterCollision = false;
        _moveDirections = transform.position;
    }

    void Update()
    {
        Move();
        SetSpeed();
    }

    void FixedUpdate()
    {
        Accelerator();
    }

    private void Move()
    {
        if (!_gameCollider.FinishedThePhase)
        {
            float directionKeyboard = _input.MovementsKeyboard();
            float directionTouch = _input.MovementsTouch();

            MoveKeyboard(directionKeyboard);
            MoveTouch(directionTouch);
        }
        else
        {
            FinishingMove();
        }
    }

    void Accelerator()
    {
        Velocity += _acceleration;

        if (Velocity < _currentSpeed)
        {
            Velocity += _acceleration;
        }
        else
        {
            Velocity = _currentSpeed;
        }
    }

    void SetSpeedAfterCollision()
    {
        _currentSpeed = _maximumSpeedAfterCollision;
        _currentSpeedMove = GetPercentage(_speed, _percentageSpeedMoveAfterCollision);
    }

    void SetSpeedAfterDoubleCollision()
    {
        _currentSpeed = _maximumSpeedAfterDoubleCollision;
        _currentSpeedMove = GetPercentage(_speed, _percentageSpeedMoveAfterDoubleCollision);
    }

    void SetSpeedNormal()
    {
        _currentSpeed = _maxSpeed;
        _currentSpeedMove = _speed;
        _isMoveAfterCollision = false;
    }

    void SetSpeed()
    {
        if (_animatorController.CountHit == 1)
        {
            SetSpeedAfterCollision();
            MoveAfterCollision();
        }
        else if (_animatorController.CountHit >= 2)
        {
            SetSpeedAfterDoubleCollision();
            MoveAfterCollision();
        }
        else
        {
            SetSpeedNormal();
        }
    }

    void FinishingMove()
    {
        _moveDirections.y += _currentSpeedMove * Time.fixedDeltaTime;
        _rb2D.MovePosition(_moveDirections);
    }

    void MoveKeyboard(float move)
    {
        if (!_input.IsTouch())
        {
            _moveDirections.x += move * _currentSpeedMove * Time.fixedDeltaTime;
            _rb2D.MovePosition(_moveDirections);
        }
    }

    void MoveTouch(float move)
    {
        if (_input.IsTouch())
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(move, transform.position.y, transform.position.z), _currentSpeedMove * Time.fixedDeltaTime);
        }
    }

    void MoveAfterCollision()
    {
        if (!_isMoveAfterCollision)
        {
            _isMoveAfterCollision = true;
            _randomDirectionAfterCollision = Random.value;
        }
        else if (_randomDirectionAfterCollision > 0)
        {
            _moveDirections += ((_randomDirectionAfterCollision > 0.5f) ? Vector3.left : Vector3.right) * Time.deltaTime;
        }
    }

    float GetPercentage(float number, float percentage)
    {
        return (number * percentage) / 100;
    }
}
