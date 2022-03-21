using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerController : MonoBehaviour
{
    PlayerInputController input;
    Rigidbody2D rb2D;
    Vector3 moveDirections;

    [SerializeField] float speed = 1.0f;
    [SerializeField] float _acceleration = 0.002f;
    public float Velocity { get; private set; }

    void Awake()
    {
        moveDirections = transform.position;
    }

    void Start()
    {
        input = GetComponent<PlayerInputController>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Accelerator();
    }

    private void Move()
    {
        float directionKeyboard = input.MovementsKeyboard();
        float directionTouch = input.MovementsTouch();

        MoveKeyboard(directionKeyboard);
        MoveTouch(directionTouch);
    }

    void Accelerator()
    {
        Velocity += _acceleration;
    }

    void MoveKeyboard(float move)
    {
        if (!input.IsTouch())
        {
            moveDirections.x += move * speed * Time.fixedDeltaTime;
            rb2D.MovePosition(moveDirections);
        }
    }

    void MoveTouch(float move)
    {
        if (input.IsTouch())
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(move, transform.position.y, transform.position.z), speed * Time.fixedDeltaTime);
        }
    }
}
