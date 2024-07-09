using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer), typeof(TimerSkill))]
public class VampirSkill : MonoBehaviour
{
    [SerializeField] private float _radios = 1f;
    [SerializeField, Min(0.1f)] private float _delay = 0.5f;
    [SerializeField, Min(0.01f)] private float _powerSkill = 0.2f;

    private List<IVampirismSubject> _vampirismSubjects;

    private TimerSkill _timerSkill;

    private Coroutine _coroutineAction;
    private WaitForSeconds _waitDelay;

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

    private void Awake()
    {
        _vampirismSubjects = new List<IVampirismSubject>();
        _waitDelay = new WaitForSeconds(_delay);
        HideSkill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IVampirismSubject vampirismSubject))
        {
            _vampirismSubjects.Add(vampirismSubject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IVampirismSubject vampirismSubject))
        {
            _vampirismSubjects.Remove(vampirismSubject);
        }
    }

    public void Activate()
    {
        if (IsCanUse)
        {
            _spriteRenderer.enabled = true;
            _circleCollider.enabled = true;

            _coroutineAction = StartCoroutine(ActionSkillRoutine());
            _timerSkill.Run();
        }
    }

    private IEnumerator ActionSkillRoutine()
    {
        float resultSkill;

        while (true)
        {
            resultSkill = 0f;

            for (int i = 0; i < _vampirismSubjects.Count; i++)
            {
                if (_vampirismSubjects[i] != null)
                    resultSkill += _vampirismSubjects[i].GetSubstractHealth(_powerSkill);
            }

            Impacted?.Invoke(resultSkill);

            yield return _waitDelay;
        }
    }

    private void Deactivate()
    {
        HideSkill();

        _vampirismSubjects.Clear();
     
        StopCoroutine(_coroutineAction);
        _coroutineAction = null;
    }

    private void HideSkill()
    {
        _spriteRenderer.enabled = false;
        _circleCollider.enabled = false;
    }
}