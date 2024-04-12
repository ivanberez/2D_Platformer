using System;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [field:SerializeField] public CollidCheker GroundCheker { get; private set; }                
    
    public Action<Player> EnemyCollideHandler;
    public Action<Coin> CoinCollideHandler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent(out Player player))
            EnemyCollideHandler?.Invoke(player);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Coin coin))
            CoinCollideHandler?.Invoke(coin);
    }
}