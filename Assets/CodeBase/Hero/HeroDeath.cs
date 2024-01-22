using System;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;

        public event Action Happened;
        
        private void OnEnable() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDisable() =>
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Happened?.Invoke();
            Destroy(gameObject);
        }
    }
}