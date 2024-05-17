using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputHandler))]
[RequireComponent(typeof(AnimationChanger), typeof(CollisionTransmitter))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private AtackElement _attackElement;

    private Mover _mover;
    private InputHandler _input;
    private AnimationChanger _animationChanger;
    private CollisionTransmitter _collisionTransmitter;

    private Wallet _wallet;
    private Health _health;

    private Transform _damageSource;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _input = GetComponent<InputHandler>();
        _animationChanger = GetComponent<AnimationChanger>();
        _collisionTransmitter = GetComponent<CollisionTransmitter>();

        _wallet = new Wallet();
        _health = new Health(_maxHealth);
    }

    private void Update()
    {
        _animationChanger.RefreshAxisesParams(_input.HorizontalAxis, _mover.VerticalAxis);
    }

    private void FixedUpdate()
    {
        if (_input.HorizontalAxis != 0)
            _mover.Move(_input.HorizontalAxis);

        if (_input.IsSpaceDown && _collisionTransmitter.GroundCheker.IsGrounded)
            _mover.Jump();

        if (_damageSource)
        {
            _mover.CastAway(_damageSource);
            _damageSource = null;
        }
    }

    private void OnEnable()
    {
        _collisionTransmitter.GroundCheker.GroundCollided += _animationChanger.ChangingOnGround;
        _collisionTransmitter.CoinCollision += _wallet.AddCoin;
        _collisionTransmitter.AidKitCollision += _health.TakeAidKit;
        _collisionTransmitter.EnemyCollision += TakeDamage;
        _input.MouseDownEvent += Attack;
        _health.DeathEvent += Death;
        _health.HealthChanged += ViewHealth;
    }

    private void OnDisable()
    {
        _collisionTransmitter.GroundCheker.GroundCollided -= _animationChanger.ChangingOnGround;
        _collisionTransmitter.CoinCollision -= _wallet.AddCoin;
        _collisionTransmitter.AidKitCollision -= _health.TakeAidKit;
        _collisionTransmitter.EnemyCollision -= TakeDamage;
        _input.MouseDownEvent -= Attack;
        _health.DeathEvent -= Death;
    }

    private void ViewHealth(int health)
    {
        Debug.Log("Health player: " + health);
    }

    private void Attack()
    {
        _animationChanger.ViewAttack();
        _attackElement.Attack();
    }

    private void TakeDamage(Enemy enemy)
    {
        _health.TakeDamage(enemy.Damage);
        _damageSource = enemy.transform;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}