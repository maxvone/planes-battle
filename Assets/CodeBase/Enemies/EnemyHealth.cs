using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemies
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    [SerializeField] private float _current;

    [SerializeField] private float _max;

    public event Action HealthChanged;

    public float Current
    {
      get => _current;
      set => _current = value;
    }

    public float Max
    {
      get => _max;
      set => _max = value;
    }

    public void TakeDamage(float damage)
    {
      Current -= damage;
      HealthChanged?.Invoke();
    }

  }
}