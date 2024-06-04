using UnityEngine;

[RequireComponent(typeof(PointsMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _attackSpeed = 10f;
    [SerializeField] private DetectingZone _detectingZone;

    private PointsMover _pointsMover;
    
    [field: SerializeField, Min(1)] public int Damage { get; private set; } = 1;

    private void Awake()
    {
        _pointsMover = GetComponent<PointsMover>();
    }

    private void Update()
    {             
        if (HasLooking(out Player player))
            Attack(player);
        else
            _pointsMover.Move();
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    private void Attack(Player player)
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _attackSpeed * Time.deltaTime);
    }

    private bool HasLooking(out Player player)
    {
        if(_detectingZone.Player != null)
        {
            player = _detectingZone.Player;
            float directionToPlayer = player.transform.position.x - transform.position.x;
            float moveDirection = _pointsMover.TargetPosition.x - transform.position.x;

            return Mathf.Sign(directionToPlayer) == Mathf.Sign(moveDirection);         
        }
        
        player = null;
        return false;
    }   
}