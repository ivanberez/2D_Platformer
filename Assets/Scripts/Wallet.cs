using UnityEngine;

[RequireComponent(typeof(CollisionTransmitter))]
public class Wallet : MonoBehaviour 
{
    private CollisionTransmitter _collisionTransmitter;  
    private int _coins;

    private void Awake() => _collisionTransmitter = GetComponent<CollisionTransmitter>();    

    private void OnEnable() => _collisionTransmitter.CoinCollision += AddCoin;

    private void OnDisable() => _collisionTransmitter.CoinCollision -= AddCoin;
    
    private void AddCoin(Coin coin)
    {
        _coins += coin.ToPickUp();
        Debug.Log("Coins: " + _coins);
    }
}