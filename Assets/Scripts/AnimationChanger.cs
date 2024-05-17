using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationChanger : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string IsGround = nameof(IsGround);
    private const string Attacked = nameof(Attacked);

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangingOnGround(bool isGround)
    {
        _animator.SetBool(IsGround, isGround);
    }

    public void RefreshAxisesParams(float x, float y)
    {
        _animator.SetFloat(Horizontal, Mathf.Abs(x));
        _animator.SetFloat(Vertical, y);

        if (x != 0)
            transform.eulerAngles = ChangerRotation.GetAxisDirection(x);
    }

    public void ViewAttack()
    {        
        _animator.SetTrigger(Attacked);
    }
}