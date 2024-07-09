using System;

public interface IDataIndication
{
    event Action Changed;
    event Action Ending;

    float Curent { get; }
    float Max { get; }
}