using UnityEngine;

[RequireComponent(typeof(PointsMover), typeof(Health))]
public class Enemy : MonoBehaviour, IVampirismSubject
{
    [SerializeField] private float _attackSpeed = 10f;    
    [SerializeField] private LayerMask _layerMaskAttack;

    private PointsMover _pointsMover;
    private Health _health;
    
    [field: SerializeField, Min(1)] public int Damage { get; private set; } = 1;
    public Vector2 Position => transform.position;

    private void Awake()
    {
        _pointsMover = GetComponent<PointsMover>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {             
        //if (HasLooking(out Player player))
        //    Attack(player);
        ////else
        //    _pointsMover.Move();
    }

    private void OnEnable()
    {
        _health.Ending += Die;
    }

    private void OnDisable()
    {
        _health.Ending -= Die;
    }

    public void TakeDamage(float damage)
    {         
        _health.Subtract(damage);        
    }

    private void Attack(Player player)
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _attackSpeed * Time.deltaTime);
    }

    private bool HasLooking(out Player player)
    {
        float distance = _pointsMover.TargetPosition.x - transform.position.x;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distance, _layerMaskAttack);        

        if(hit)
        {            
            return hit.collider.TryGetComponent(out player);
        }
        else
        {
            player = null;
            return false; 
        }   
    }  
    
    private void Die()
    {
        Destroy(gameObject);
    }

    public float GetSubstractHealth(float impactPower) => _health.Subtract(impactPower);    
}