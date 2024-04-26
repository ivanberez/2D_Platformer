using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationChanger : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string IsGround = nameof(IsGround);    

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

        ChangerRotation.DefineAxisX(x, transform);
    }
}