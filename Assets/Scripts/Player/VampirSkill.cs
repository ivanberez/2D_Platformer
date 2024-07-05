using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer), typeof(TimerSkill))]
public class VampirSkill : MonoBehaviour
{
    [SerializeField] private float _radios = 1f;
    [SerializeField, Min(0.01f)] private float _powerSkill = 0.2f;

    private TimerSkill _timerSkill;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;    

    public bool IsCanUse => _timerSkill.IsWork == false;

    public event Action<float> Impacted;

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _timerSkill = GetComponent<TimerSkill>();

        _circleCollider.radius = _radios;
        _circleCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        _timerSkill.ActionFinishing += Deactivate;
    }

    private void OnDisable()
    {
        _timerSkill.ActionFinishing -= Deactivate;
    }

    private void Start() => Deactivate();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_powerSkill);
            Impacted?.Invoke(_powerSkill);
        }
    }

    public void Activate()
    {
        if(IsCanUse)
        {
            _spriteRenderer.enabled = true;
            _circleCollider.enabled = true;

            _timerSkill.Run();
        }
    }

    private void Deactivate()
    {
        _spriteRenderer.enabled = false;
        _circleCollider.enabled = false;
    }      
}