using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CollisionChecker))]
public class Movement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed;
    [SerializeField] private float _velPower;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _forceJump;    

    private Rigidbody2D _rigidbody2D;
    private CollisionChecker _collisionChecker;
    
    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collisionChecker = GetComponent<CollisionChecker>();
    }

    private void Update()
    {
        HorizontalAxis = Input.GetAxis(Horizontal);
        VerticalAxis = _rigidbody2D.velocity.y;

        if (HorizontalAxis != 0)
            Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Move()
    {
        float targetSpeed = HorizontalAxis  * _speed;
        float speedDif = targetSpeed - _rigidbody2D.velocity.x;
        float leration = (Mathf.Abs(targetSpeed) > 0.01) ? _acceleration : _decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * leration, _velPower) * Mathf.Sign(speedDif);

        _rigidbody2D.AddForce(movement * Vector2.right);        
    }

    private void Jump()
    {
        if (_collisionChecker.GroundCheker.IsCollided)
            _rigidbody2D.AddForce(transform.up * _forceJump, ForceMode2D.Impulse);
    }
}