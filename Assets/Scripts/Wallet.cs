using UnityEngine;

public class Wallet
{
    private int _coins;

    public void AddCoin(Coin coin)
    {
        _coins += coin.ToPickUp();
        Debug.Log("Coins: " + _coins);
    }
}