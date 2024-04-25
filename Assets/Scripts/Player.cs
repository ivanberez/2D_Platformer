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
        _mover.CommandMove(_input.HorizontalAxis);
        _animationChanger.RefreshAxisesParams(_mover.HorizontalAxis, _mover.VerticalAxis);
    }

    private void OnEnable()
    {
        _input.SpaceDown += ()=> _mover.CommandJump(_collisionTransmitter.IsGrounded); 
        _collisionTransmitter.GroundCollision += _animationChanger.ChangingOnGround;
        _collisionTransmitter.CoinCollision += _wallet.AddCoin;
    }

    private void OnDisable()
    {
        _collisionTransmitter.GroundCollision -= _animationChanger.ChangingOnGround;
        _collisionTransmitter.CoinCollision -= _wallet.AddCoin;
    }
}