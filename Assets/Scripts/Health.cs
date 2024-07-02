﻿using System;
using UnityEngine;

public class Health : MonoBehaviour, IDataIndication
{
    public event Action Ending;
    public event Action Changed;

    [SerializeField, Min(0)] private float _curent = 1;
    [SerializeField, Min(0)] private float _max = 1;

    public bool IsUnwell => _curent < _max;
    public float Curent
    {
        get => _curent;
        private set
        {
            _curent = value;
            Changed?.Invoke();

            if (_curent <= 0)
                Ending?.Invoke();
        }
    }
    public float Max => _max;

    private void Start() => Curent = Max;

    public void Add(float count) => Curent += Mathf.Clamp(count, 0, Max - Curent);

    public void Subtract(float count) => Curent -= Mathf.Clamp(count, 0, Curent);
}