using System;
using UnityEngine;

public class CollidCheker : MonoBehaviour
{    
    public bool IsCollided { get; private set; }

    public Action<bool> CollidHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollidHandler?.Invoke(IsCollided = true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollidHandler?.Invoke(IsCollided = false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollidHandler?.Invoke(IsCollided = true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollidHandler?.Invoke(IsCollided = false);
    }
}
