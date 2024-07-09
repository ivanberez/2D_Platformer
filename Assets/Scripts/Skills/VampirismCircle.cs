using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
    public class VampirismCircle : MonoBehaviour
    {
        [SerializeField] private float _radios;

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider;
        private List<IVampirismSubject> _subjectList;        

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider = GetComponent<CircleCollider2D>();
            _subjectList = new List<IVampirismSubject>();

            _circleCollider.radius = _radios;
            _circleCollider.isTrigger = true;

            Disable();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IVampirismSubject vampirismSubject))
            {
                _subjectList.Add(vampirismSubject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IVampirismSubject vampirismSubject))
            {
                _subjectList.Remove(vampirismSubject);
            }
        }

        public bool TryGetNearSubject(out IVampirismSubject nearSubject)
        {
            if (_subjectList.Count > 0)
            {
                if (_subjectList.Count == 1)
                {
                    nearSubject = _subjectList.First();
                }
                else
                {
                    float minDistance = _subjectList.Min(subject => Vector2.Distance(transform.position, subject.Position));
                    nearSubject = _subjectList.FirstOrDefault(subject => Vector2.Distance(transform.position, subject.Position).Equals(minDistance));
                }
            }
            else
            {
                nearSubject = null;
            }

            return nearSubject != null;
        }

        public void Enable()
        {
            _circleCollider.radius = Mathf.Lerp(_circleCollider.radius, _radios, 1);
            _spriteRenderer.enabled = true;
        }

        public void Disable()
        {
            _spriteRenderer.enabled = false;
            _circleCollider.radius = 0;
            _subjectList.Clear();
        }
    }
}