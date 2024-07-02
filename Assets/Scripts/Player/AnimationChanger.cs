using UnityEngine;

[RequireComponent(typeof(Animator), typeof(InputHandler), typeof(CollisionTransmitter))]
public class AnimationChanger : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string IsGround = nameof(IsGround);
    private const string Attacked = nameof(Attacked);

    private Animator _animator;
    private InputHandler _inputHandler;
    private CollisionTransmitter _collisionTransmitter;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputHandler = GetComponent<InputHandler>();
        _collisionTransmitter = GetComponent<CollisionTransmitter>();
    }

    private void Update()
    {
        RefreshAxisesParams(_inputHandler.HorizontalAxis, _inputHandler.VerticalAxis);
    }

    private void OnEnable()
    {
        _collisionTransmitter.GroundCheker.GroundCollided += ChangingOnGround;
        _inputHandler.MouseDownEvent += ViewAttack;
    }

    private void OnDisable()
    {
        _collisionTransmitter.GroundCheker.GroundCollided -= ChangingOnGround;
        _inputHandler.MouseDownEvent -= ViewAttack;
    }

    private void ChangingOnGround(bool isGround)
    {
        _animator.SetBool(IsGround, isGround);
    }

    private void RefreshAxisesParams(float horizontal, float vertical)
    {
        _animator.SetFloat(Horizontal, Mathf.Abs(horizontal));
        _animator.SetFloat(Vertical, vertical);

        if (horizontal != 0)
            transform.eulerAngles = ChangerRotation.GetAxisDirection(horizontal);
    }

    private void ViewAttack()
    {        
        _animator.SetTrigger(Attacked);
    }
}