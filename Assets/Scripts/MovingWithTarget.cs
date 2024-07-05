using UnityEngine;

public class MovingWithTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetX = 0f;
    [SerializeField] private float _offsetY = 0f;

    private void Update()
    {
        if (isActiveAndEnabled && _target != null)
            transform.position = new Vector2(_target.transform.position.x + _offsetX, _target.transform.position.y + _offsetY);
    }
}
