using System;
using System.Collections;
using UnityEngine;

public class TimerSkill : MonoBehaviour, IDataIndication
{
    private readonly WaitForSecondsRealtime WaitSecond = new WaitForSecondsRealtime(1);

    [SerializeField, Min(0.1f)] private float _timeAction;
    [SerializeField, Min(0.1f)] private float _timeCooldown;

    private float _curent;
    private float _max;
    private Coroutine _coroutine;

    public event Action Changed;
    public event Action Ending;
    public event Action ActionFinishing;

    public bool IsWork => _coroutine != null;

    public float Curent
    {
        get => _curent;
        private set
        {
            _curent = value;
            Changed?.Invoke();
        }
    }

    public float Max => _max;

    private void Awake() => _curent = _max = _timeAction;

    public void Run()
    {
        _coroutine = StartCoroutine(WorkRoutine());
    }

    private IEnumerator WorkRoutine()
    {
        yield return StartCoroutine(ActionRoutine());
        ActionFinishing?.Invoke();

        yield return StartCoroutine(CooldownRoutine());
        Ending?.Invoke();

        _coroutine = null;
    }

    private IEnumerator ActionRoutine()
    {
        _curent = _timeAction;
        _max = _timeAction;

        while (_curent > 0)
        {
            Curent--;
            yield return WaitSecond;            
        }
    }

    private IEnumerator CooldownRoutine()
    {
        _curent = 0;
        _max = _timeCooldown;

        while (_curent < _timeCooldown)
        {
            Curent++;
            yield return WaitSecond;            
        }
    }
}