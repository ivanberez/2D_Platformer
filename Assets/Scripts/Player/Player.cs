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

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
        _input = GetComponent<InputHandler>();
        _collisionTransmitter = GetComponent<CollisionTransmitter>();        
    }

    private void FixedUpdate()
    {
        if (_input.HorizontalAxis != 0)
            _mover.Move(_input.HorizontalAxis);

        if (_input.IsSpaceDown && _collisionTransmitter.GroundCheker.IsGrounded)
            _mover.Jump();
    }

    private void OnEnable()
    {
        _collisionTransmitter.EnemyCollision += TakeDamage;
        _collisionTransmitter.AidKitCollision += TakeAidKit;
        _health.Ending += Death;
        _vampirSkill.Impacted += _health.Add;
    }

    private void OnDisable()
    {
        _collisionTransmitter.EnemyCollision -= TakeDamage;
        _health.Ending -= Death;
        _vampirSkill.Impacted -= _health.Add;
    }

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