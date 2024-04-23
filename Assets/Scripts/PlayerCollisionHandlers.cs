using System;
using UnityEngine;

public class PlayerCollisionHandlers : MonoBehaviour
{
    public event Action<Enemy> EnemyCollideHandler;
    public event Action<Coin> CoinCollideHandler;

    [field:SerializeField] public CollidCheker GroundCheker { get; private set; }                       

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent(out Enemy enemy))
            EnemyCollideHandler?.Invoke(enemy);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Coin coin))
            CoinCollideHandler?.Invoke(coin);
    }
}