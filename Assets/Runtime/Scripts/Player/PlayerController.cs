using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerController : MonoBehaviour
{
    PlayerInputController _input;
    PlayerCollidersController _gameCollider;
    Rigidbody2D _rb2D;
    Vector3 _moveDirections;

    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _acceleration = 0.002f;
    public float Velocity { get; private set; }

    void Awake()
    {
        _input = GetComponent<PlayerInputController>();
        _gameCollider = FindObjectOfType<PlayerCollidersController>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _moveDirections = transform.position;
    }

    void Update()
    {
        Move();
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

        if (Velocity < _maxSpeed)
        {
            Velocity += _acceleration;
        }
        else
        {
            Velocity = _maxSpeed;
        }
    }

    void FinishingMove()
    {
        _moveDirections.y += _speed * Time.fixedDeltaTime;
        _rb2D.MovePosition(_moveDirections);
    }

    void MoveKeyboard(float move)
    {
        if (!_input.IsTouch())
        {
            _moveDirections.x += move * _speed * Time.fixedDeltaTime;
            _rb2D.MovePosition(_moveDirections);
        }
    }

    void MoveTouch(float move)
    {
        if (_input.IsTouch())
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(move, transform.position.y, transform.position.z), _speed * Time.fixedDeltaTime);
        }
    }
}
