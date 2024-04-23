using System;
using UnityEngine;

public class CollidCheker : MonoBehaviour
{
    [SerializeField] private LayerMask _callbackLayersMask;

    public event Action<bool> CollidHandler;

    private void OnValidate()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.callbackLayers = _callbackLayersMask;
        collider2D.contactCaptureLayers = _callbackLayersMask;
    }

    public bool IsCollided { get; private set; }    

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
