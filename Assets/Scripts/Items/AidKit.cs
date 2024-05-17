using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField, Min(1)] private int _additionalHealth;

    public int PickUp()
    {
        Destroy(gameObject);
        return _additionalHealth;
    }
}
