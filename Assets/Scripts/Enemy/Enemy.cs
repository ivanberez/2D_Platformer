using UnityEngine;

[RequireComponent(typeof(PointsMover))]
public class Enemy : MonoBehaviour
{    
    private PointsMover _pointsMover;

    [field: SerializeField, Min(1)] public int Damage { get; private set; } = 1;

    private void Awake()
    {
        _pointsMover = GetComponent<PointsMover>();
    }
    private void Update()
    {
        if (TryDetectPlayer(out Player player))
            Attack(player);
        else
            _pointsMover.Move();
    }

    private void Attack(Player player)
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
    }

    private bool TryDetectPlayer(out Player player)
    {
        Vector2 direction = _pointsMover.TargetPosition - transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

        foreach (RaycastHit2D hit in hits)
            if (hit.collider == null)
                continue;
            else if (hit.transform.TryGetComponent(out player))
                return true;

        player = null;
        return false;
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
