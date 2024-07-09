using Assets.Scripts.Skills;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Health), typeof(InputHandler))]
[RequireComponent(typeof(AnimationChanger), typeof(CollisionTransmitter), typeof(Wallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private VampirSkill _vampirSkill;

    private Mover _mover;
    private Health _health;
    private InputHandler _input;
    private CollisionTransmitter _collisionTransmitter;    
    
    private bool _isCanJump;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
        _input = GetComponent<InputHandler>();
        _collisionTransmitter = GetComponent<CollisionTransmitter>();        
    }

    private void FixedUpdate()
    {
        Move();                
        Jump();
    }    

    private void OnEnable()
    {
        _collisionTransmitter.EnemyCollision += TakeDamage;
        _collisionTransmitter.AidKitCollision += TakeAidKit;
        _health.Ending += Death;
        _input.JumpKeyDownEvent += DownJumpKey;
        _input.VampirKeyDownEvent += _vampirSkill.Activate;
        _vampirSkill.Impacted += _health.Add;
    }

    private void OnDisable()
    {
        _collisionTransmitter.EnemyCollision -= TakeDamage;
        _collisionTransmitter.AidKitCollision -= TakeAidKit;
        _health.Ending -= Death;
        _input.JumpKeyDownEvent -= DownJumpKey;
        _input.VampirKeyDownEvent -= _vampirSkill.Activate;
        _vampirSkill.Impacted -= _health.Add;
    }

    private void Move()
    {
        _mover.Move(_input.HorizontalAxis);
    }

    private void Jump()
    {
        if (_isCanJump)
        {
            _mover.Jump();
            _isCanJump = false;
        }
    }

    private void DownJumpKey() => _isCanJump = _collisionTransmitter.GroundCheker.IsGrounded;

    private void TakeAidKit(AidKit kit)
    {
        if (_health.IsUnwell)
            _health.Add(kit.ToPickUp());
    }

    private void TakeDamage(Enemy enemy)
    {
        _health.Subtract(enemy.Damage);
        _mover.CastAway(enemy);
    }

    private void Death()
    {
        Destroy(gameObject);
    }    
}