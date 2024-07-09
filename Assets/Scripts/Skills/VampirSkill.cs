using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(VampirismCircle), typeof(TimerSkill))]
    public class VampirSkill : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _delay = 0.5f;
        [SerializeField, Min(0.01f)] private float _powerSkill = 0.2f;

        private VampirismCircle _circle;
        private TimerSkill _timerSkill;

        private Coroutine _coroutineAction;
        private WaitForSeconds _waitDelay;

        public event Action<float> Impacted;

        public bool IsCanUse => _timerSkill.IsWork == false;

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
            _timerSkill = GetComponent<TimerSkill>();
            _circle = GetComponent<VampirismCircle>();

            _waitDelay = new WaitForSeconds(_delay);
        }

        public void Activate()
        {
            if (IsCanUse)
            {
                _circle.Enable();
                _coroutineAction = StartCoroutine(ActionSkillRoutine());
                _timerSkill.Run();
            }
        }

        private IEnumerator ActionSkillRoutine()
        {
            while (enabled)
            {
                if (_circle.TryGetNearSubject(out IVampirismSubject target))
                    Impacted?.Invoke(target.GetSubstractHealth(_powerSkill));

                yield return _waitDelay;
            }
        }

        private void Deactivate()
        {
            _circle.Disable();
            StopCoroutine(_coroutineAction);
            _coroutineAction = null;
        }
    }
}