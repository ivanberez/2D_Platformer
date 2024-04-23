using System;
using UnityEngine;

public class PlayerCollisionHandlers : MonoBehaviour
{
    public event Action<Player> EnemyCollideHandler;
    public event Action<Coin> CoinCollideHandler;

    [field:SerializeField] public CollidCheker GroundCheker { get; private set; }                       

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