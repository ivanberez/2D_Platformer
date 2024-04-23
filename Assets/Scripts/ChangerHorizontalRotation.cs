using UnityEngine;

public class ChangerHorizontalRotation
{
    private readonly Vector3 RightRotation = Vector3.zero;
    private readonly Vector3 LeftRotation = new Vector3(0, 180, 0);

    private Transform _transform;

    public ChangerHorizontalRotation(Transform transform)
    {
        _transform = transform;
    }
    public void DefineRotation(float xDirection)
    {
        if (xDirection == 0)
            return;
        else
            _transform.eulerAngles = xDirection > 0 ? RightRotation : LeftRotation;
    }
}