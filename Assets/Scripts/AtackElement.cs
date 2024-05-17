using UnityEngine;

public class AtackElement : MonoBehaviour
{
    private readonly float _angle = 0;

    [SerializeField] private Vector2 _size = new Vector2(1.8f, 0.5f);
    [SerializeField] private bool _isDrow;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _size, _angle);

        foreach (Collider2D hit in hits)
        {            
            if (hit.transform.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage();
        }
    }

    private void OnDrawGizmos()
    {
        if (_isDrow)
            Gizmos.DrawCube(transform.position, _size);
    }
}