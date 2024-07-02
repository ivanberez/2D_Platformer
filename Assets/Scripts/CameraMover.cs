using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector2 _maxPositon;
    [SerializeField] private Vector2 _minPositon;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 1;

    private void OnValidate()
    {
        Update();
    }

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

        result.x = Mathf.Clamp(result.x, _minPositon.x, _maxPositon.x);
        result.y = Mathf.Clamp(result.y, _minPositon.y, _maxPositon.y);
        result.z = transform.position.z;

        return result;
    }
}
