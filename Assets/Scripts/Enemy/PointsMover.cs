using UnityEngine;

public class PointsMover : MonoBehaviour
{
    private const int StartIndexPoint = 0;

    [SerializeField] private float _speed = 1;
    [SerializeField] protected Transform _parentPoints;

    private int _indexPoint;    
    private Transform[] _points;

    public Vector3 TargetPosition { get; private set; }

    private void OnValidate()
    {
        if (_parentPoints == null)
        {
            Debug.LogWarning($"No parent points have been assigned for {gameObject.name}", gameObject);
            enabled = false;
        }
        else
        {
            _points = new Transform[_parentPoints.childCount];

            if (_points.Length < 1)
            {
                Debug.LogWarning($"Need more points path {gameObject.name}", gameObject);
                enabled = false;
            }
            else
            {
                for (int i = 0; i < _points.Length; i++)
                    _points[i] = _parentPoints.GetChild(i).transform;

                enabled = true;
            }
        }
    }

    private void Start()
    {
        _indexPoint = StartIndexPoint;
        TargetPosition = _points[_indexPoint].position;
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _speed * Time.deltaTime);

        if (transform.position == TargetPosition)
            SetTargetPoint();
    }

    private void SetTargetPoint()
    {
        _indexPoint = ++_indexPoint % _points.Length;
        TargetPosition = _points[_indexPoint].position;
        
        transform.eulerAngles = ChangerRotation.GetAxisDirection(transform.position.x - TargetPosition.x);
    }
}
