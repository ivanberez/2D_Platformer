using UnityEngine;

public interface IVampirismSubject
{
    float GetSubstractHealth(float impactPower);
    Vector2 Position { get; }
}
