using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class View : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string IsGround = nameof(IsGround);

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _spriteRenderer.flipX = x < 0;
    }
}