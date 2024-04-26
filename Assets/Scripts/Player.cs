using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputHandler))]
[RequireComponent(typeof(AnimationChanger), typeof(CollisionTransmitter))]
public class Player : MonoBehaviour
{
    private Mover _mover;
    private InputHandler _input;
    private AnimationChanger _animationChanger;
    private CollisionTransmitter _collisionTransmitter;
    private Wallet _wallet;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _input = GetComponent<InputHandler>();
        _animationChanger = GetComponent<AnimationChanger>();
        _collisionTransmitter = GetComponent<CollisionTransmitter>();

        _wallet = new Wallet();
    }

    private void Update()
    {
        _animationChanger.RefreshAxisesParams(_input.HorizontalAxis, _mover.VerticalAxis);
    }

    private void FixedUpdate()
    {
        if (_input.HorizontalAxis != 0)
            _mover.Move(_input.HorizontalAxis);        
    }

    private void OnEnable()
    {
        _input.SpaceDown += Jump;
        _collisionTransmitter.GroundCheker.GroundCollided += _animationChanger.ChangingOnGround;
        _collisionTransmitter.CoinCollision += _wallet.AddCoin;
    }

    private void OnDisable()
    {   
        _input.SpaceDown -= Jump;
        _collisionTransmitter.GroundCheker.GroundCollided -= _animationChanger.ChangingOnGround;
        _collisionTransmitter.CoinCollision -= _wallet.AddCoin;
    }

    private void Jump()
    {
        if (_collisionTransmitter.GroundCheker.IsGrounded)
            _mover.Jump();
    }
}