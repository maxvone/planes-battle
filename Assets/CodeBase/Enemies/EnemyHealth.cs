using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemies
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;
    public float Current
    {
      get => _current;
      set
      {
        if (Math.Abs(value - _current) < Mathf.Epsilon) return;
        
        _current = value;
          
        HealthChanged?.Invoke();
      }
    }

    public float Max
    {
      get => _max;
      set => _max = value;
    }

    [SerializeField] private float _max;
    private float _current;

    private void Start() => 
      ResetHealth();

    public void ResetHealth() => 
      _current = _max;

    public void TakeDamage(float damage)
    {
      if(Current <= 0)
        return;
      
      Current -= damage;
    }
  }
}