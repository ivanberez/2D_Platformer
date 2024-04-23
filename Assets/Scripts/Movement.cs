using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollisionHandlers))]
public class Movement : MonoBehaviour
{  
    [SerializeField] private float _speed;
    [SerializeField] private float _velPower;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _forceJump;

    private Rigidbody2D _rigidbody2D;
    private PlayerCollisionHandlers _collisionChecker;
    private InputHandler _inputHandler;

    public float HorizontalAxis => _inputHandler.HorizontalInput;
    public float VerticalAxis  =>_rigidbody2D.velocity.y;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collisionChecker = GetComponent<PlayerCollisionHandlers>();
        _inputHandler = GetComponent<InputHandler>();
    }    

    private void FixedUpdate()
    {
        if (HorizontalAxis != 0)
        {
            float targetSpeed = HorizontalAxis * _speed;
            float speedDif = targetSpeed - _rigidbody2D.velocity.x;
            float leration = (Mathf.Abs(targetSpeed) > 0.01) ? _acceleration : _decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * leration, _velPower) * Mathf.Sign(speedDif);

            _rigidbody2D.AddForce(movement * Vector2.right);
        }
    }

    private void OnEnable()
    {
        _inputHandler.SpaceDown += Jump;
    }

    private void OnDisable()
    {
        _inputHandler.SpaceDown -= Jump;
    }

    private void Jump()
    {
        if (_collisionChecker.GroundCheker.IsCollided)
            _rigidbody2D.AddForce(transform.up * _forceJump, ForceMode2D.Impulse);
    }
}