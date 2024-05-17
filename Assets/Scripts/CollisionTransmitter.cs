using System;
using UnityEngine;

public class CollisionTransmitter : MonoBehaviour
{    
    public event Action<Enemy> EnemyCollision;
    public event Action<Coin> CoinCollision;
    public event Action<AidKit> AidKitCollision;

    [field: SerializeField] public GroundCheker GroundCheker { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {                
        if(collision.transform.TryGetComponent(out Enemy enemy))
            EnemyCollision?.Invoke(enemy);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Coin coin))
            CoinCollision?.Invoke(coin);

        if (collision.transform.TryGetComponent(out AidKit aidKit))
            AidKitCollision?.Invoke(aidKit);
    }
}