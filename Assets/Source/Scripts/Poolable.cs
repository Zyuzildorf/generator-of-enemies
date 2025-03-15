using System;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    
    public event Action<Poolable> OnPreferRelease;

    private void OnEnable()
    {
        _enemy.OnTriggerEntered += InvokeEvent;
    }

    private void OnDisable()
    {
        _enemy.OnTriggerEntered -= InvokeEvent;
    }

    private void InvokeEvent()
    {
        OnPreferRelease?.Invoke(this);
    }
}