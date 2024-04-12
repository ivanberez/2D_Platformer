using UnityEngine;

public class AroundPlatformMover : MonoBehaviour
{
    private readonly Vector3 RightRotation = Vector3.zero;
    private readonly Vector3 LeftRotation = new Vector3(0, 180, 0);

    [SerializeField] private float _speed = 1;
    [SerializeField] private CollidCheker _groundCheker;   

    private void Awake()
    {
        _groundCheker.CollidHandler += CheckingPlatform;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void CheckingPlatform(bool isGround)
    {        
        if (isGround == false)
            transform.eulerAngles = transform.eulerAngles == RightRotation ? LeftRotation : RightRotation;
    }
}
