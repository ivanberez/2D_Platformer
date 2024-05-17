using UnityEngine;

public static class ChangerRotation
{
    private static Vector3 RightRotation = Vector3.zero;
    private static Vector3 LeftRotation = new Vector3(0, 180, 0);

    public static Vector3 GetAxisDirection(float xDirection)
    {
        return xDirection > 0 ? RightRotation : LeftRotation;
    }
}