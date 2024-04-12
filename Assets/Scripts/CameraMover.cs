using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector2 _maxPositon;
    [SerializeField] private Vector2 _minPositon;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 1;
    
    private void Update()
    {
        if (_target)
        {
            Vector3 targetPosition = AdjustByBorders(_target.position);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
        }            
    }

    private Vector3 AdjustByBorders(Vector2 position)
    {
        Vector3 result = position;
        result.z = transform.position.z;

        if(position.x > _maxPositon.x) 
            result.x = _maxPositon.x;
        else if(position.x < _minPositon.x)
            result.x = _minPositon.x;

        if (position.y > _maxPositon.y)
            result.y = _maxPositon.y;
        else if (position.y < _minPositon.y)
            result.y = _minPositon.y;

        return result;
    }
}
