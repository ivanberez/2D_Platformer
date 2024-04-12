using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Movement), typeof(View))]
public class Player : MonoBehaviour
{
    private View _view;
    private Movement _movement;        
    private CollisionChecker _collisionChecker;

    private int _coins = 0;

    private void Awake()
    {
        _view = GetComponent<View>();
        _movement = GetComponent<Movement>();
        _collisionChecker = GetComponent<CollisionChecker>();
    }

    private void Update()
    {
        _view.RefreshAxisesParams(_movement.HorizontalAxis, _movement.VerticalAxis); 
    }

    private void OnEnable()
    {
        _collisionChecker.GroundCheker.CollidHandler += _view.ChangingOnGround;
        _collisionChecker.CoinCollideHandler += PickUpCoin;
    }

    private void OnDisable()
    {
        _collisionChecker.GroundCheker.CollidHandler -= _view.ChangingOnGround;
        _collisionChecker.CoinCollideHandler -= PickUpCoin;
    }

    private void PickUpCoin(Coin coin)
    {
        _coins += coin.ToPickUp();
        Debug.Log("Coins: " + _coins); 
    }
}
