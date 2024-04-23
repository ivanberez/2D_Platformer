using UnityEngine;

[RequireComponent(typeof(Movement), typeof(View))]
public class Player : MonoBehaviour
{
    private View _view;
    private Movement _movement;        
    private PlayerCollisionHandlers _collisionChecker;
    private Wallet _wallet;     

    private void Awake()
    {        
        _view = GetComponent<View>();
        _movement = GetComponent<Movement>();
        _collisionChecker = GetComponent<PlayerCollisionHandlers>();        

        _wallet = new Wallet();
    }

    private void Update()
    {       
        _view.RefreshAxisesParams(_movement.HorizontalAxis, _movement.VerticalAxis);                
    }

    private void OnEnable()
    {
        _collisionChecker.GroundCheker.CollidHandler += _view.ChangingOnGround;
        _collisionChecker.CoinCollideHandler += _wallet.AddCoin;
    }

    private void OnDisable()
    {
        _collisionChecker.GroundCheker.CollidHandler -= _view.ChangingOnGround;
        _collisionChecker.CoinCollideHandler -= _wallet.AddCoin;
    }
}
