using System;
using UnityEngine;

public interface IDataIndication
{
    event Action Changed;
    event Action Ending;

    float Curent { get; }
    float Max { get; }
}