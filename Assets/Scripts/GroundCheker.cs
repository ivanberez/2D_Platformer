using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundCheker : MonoBehaviour
{   
    [SerializeField] private LayerMask _callbackLayersMask;    

    public event Action<bool> GroundCollided;

    public bool IsGrounded { get; private set; }

    private void OnValidate()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.callbackLayers = _callbackLayersMask;
        collider.contactCaptureLayers = _callbackLayersMask;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundCollided?.Invoke(IsGrounded = true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GroundCollided?.Invoke(IsGrounded = false);
    }
}
