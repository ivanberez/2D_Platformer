using System;
using UnityEngine;

[RequireComponent(typeof(CollisionTransmitter))]
public class Health : MonoBehaviour 
{
    [SerializeField] private int _max = 3;
    private int _count;
    private CollisionTransmitter _transmitter;
    
    public event Action DeathEvent;

    private void Awake()
    {
        _count = _max;
        _transmitter = GetComponent<CollisionTransmitter>();
    }    

    private void OnEnable()    
    {        
        _transmitter.EnemyCollision += TakeDamage;
        _transmitter.AidKitCollision += TakeAidKit;
    }

    private void OnDisable()
    {
        _transmitter.EnemyCollision -= TakeDamage;
        _transmitter.AidKitCollision -= TakeAidKit;
    }

    private void TakeAidKit(AidKit aidKit)
    {
        if (_count >= _max)
        {
            return;
        }
        else
        {
            _count += aidKit.PickUp();            

            if (_count > _max)
                _count = _max;

            Debug.Log("Take aid kit. Health: " + _count);
        }                    
    }

    private void TakeDamage(Enemy ememy)
    {
        _count -= ememy.Damage;        
        Debug.Log("Take damage. Health: " + _count);

        if (_count <= 0)
            DeathEvent?.Invoke();
    }
}