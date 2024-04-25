using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
    private const string PickUp = nameof(PickUp);

    [SerializeField, Min(1)] private int _count = 1;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public int ToPickUp()
    {
        _animator.SetTrigger(PickUp);
        return _count;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}