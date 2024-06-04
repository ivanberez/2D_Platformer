using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Item : MonoBehaviour
{
    private const string PickUp = nameof(PickUp);

    [SerializeField, Min(1)] private int _count = 1;
    [SerializeField, Min(0.1f)] private float _timeDestroy;

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public int ToPickUp()
    {
        _animator.SetTrigger(PickUp);
        Destroy(gameObject, _timeDestroy);
        return _count;
    }
}