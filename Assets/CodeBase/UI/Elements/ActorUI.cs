using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private UIHealth _healthBar;
    
        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
            _healthBar.Construct(health.Max);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar() => 
            _healthBar.SetValue(_health.Current);
    }
}
