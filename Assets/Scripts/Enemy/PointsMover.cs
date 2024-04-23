using UnityEngine;

public class PointsMover : MonoBehaviour
{
    private const int StartIndexPoint = 0;

    [SerializeField] private float _speed = 1;
    [SerializeField] protected Transform _parentPoints;

    private int _indexPoint;
    private Vector3 _targetPosition;
    private Transform[] _points;
    private ChangerHorizontalRotation _horizontalRotation;

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

    private void Awake()
    {
        _horizontalRotation = new ChangerHorizontalRotation(transform);
    }

    private void Start()
    {
        _indexPoint = StartIndexPoint;
        _targetPosition = _points[_indexPoint].position;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPosition)
            SetTargetPoint();
    }

    private void SetTargetPoint()
    {
        _indexPoint = (++_indexPoint) % _points.Length;
        _targetPosition = _points[_indexPoint].position;
        
        _horizontalRotation.DefineRotation(transform.position.x - _targetPosition.x);
    }
}
