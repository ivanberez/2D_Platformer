using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _velPower;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _forceJump;

    private Rigidbody2D _rigidbody2D;

    public float VerticalAxis => _rigidbody2D.velocity.y;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float xAxis)
    {
        float targetSpeed = xAxis * _speed;
        float speedDif = targetSpeed - _rigidbody2D.velocity.x;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * _acceleration, _velPower) * Mathf.Sign(speedDif);

        _rigidbody2D.AddForce(movement * Vector2.right);
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(transform.up * _forceJump, ForceMode2D.Impulse);
    }

    public void CastAway(Transform source)
    {
        Vector2 direction = (transform.position - source.position);             
        _rigidbody2D.AddForce(direction.normalized * _forceJump, ForceMode2D.Impulse);        
    }
}