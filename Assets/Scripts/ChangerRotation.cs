using UnityEngine;

public static class ChangerRotation
{
    private static Vector3 RightRotation = Vector3.zero;
    private static Vector3 LeftRotation = new Vector3(0, 180, 0);
    
    public static void DefineAxisX(float xDirection, Transform transform)
    {
        if (xDirection == 0)
            return;
        else
            transform.eulerAngles = xDirection > 0 ? RightRotation : LeftRotation;
    }
}