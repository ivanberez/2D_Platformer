using System;

public class Health
{
    private int _max;
    private int _curent;

    public event Action DeathEvent;
    public event Action<int> HealthChanged;

    public Health(int max)
    {
        _curent = _max = max;
    }

    public void TakeDamage(int damage)
    {
        _curent -= damage;
        HealthChanged?.Invoke(_curent);
        
        if (_curent <= 0)
            DeathEvent?.Invoke();
    }

    public void TakeAidKit(AidKit aidKit)
    {
        if (_curent >= _max)
            return;
        else
        {
            _curent += aidKit.PickUp();
            HealthChanged?.Invoke(_curent);

            if (_curent > _max)
                _curent = _max;           
        }                    
    }
}