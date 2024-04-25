using System;
using UnityEngine;

public class CollisionTransmitter : MonoBehaviour
{
    [SerializeField] private GroundCheker _groundCheker;        

    public event Action<Enemy> EnemyCollision;
    public event Action<Coin> CoinCollision;
    public event Action<bool> GroundCollision;

    public bool IsGrounded => _groundCheker.IsGrounded;    

    private void Awake()
    {
        _groundCheker.GroundCollided += (bool isGround) => GroundCollision?.Invoke(isGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {                
        if(collision.transform.TryGetComponent(out Enemy enemy))
            EnemyCollision?.Invoke(enemy);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Coin coin))
            CoinCollision?.Invoke(coin);
    }
}