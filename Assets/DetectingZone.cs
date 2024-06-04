using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectingZone : MonoBehaviour
{
    public Player Player { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
            Player = player;            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            Player = null;
    }
}
