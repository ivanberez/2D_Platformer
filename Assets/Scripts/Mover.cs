using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _velPower;
    [SerializeField] private float _acceleration;    
    [SerializeField] private float _forceJump;

    private Rigidbody2D _rigidbody2D;
    private bool _hasCouldJump;
    private bool _hasCouldMove;

    public float HorizontalAxis { get; private set; }    
    public float VerticalAxis { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        if (_hasCouldMove)
            Move();

        if (_hasCouldJump)
            Jump();

        VerticalAxis = _rigidbody2D.velocity.y;
    }

    private void Move()
    {
        float targetSpeed = HorizontalAxis * _speed;
        float speedDif = targetSpeed - _rigidbody2D.velocity.x;         
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * _acceleration, _velPower) * Mathf.Sign(speedDif);

        _rigidbody2D.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(transform.up * _forceJump, ForceMode2D.Impulse);
        _hasCouldJump = false;
    }

    public void CommandMove(in float horizontalAxis)
    {        
        _hasCouldMove = (HorizontalAxis = horizontalAxis) != 0;
    }

    public void CommandJump(bool isGrounded)
    {
        _hasCouldJump = isGrounded;
    }
}