using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private UIHealth _healthBar;
        [SerializeField] private UIScore _scoreBar;
        
        
        private IHealth _health;
        private IScoreCounter _scoreCounter;

        public void Construct(IHealth health, IScoreCounter scoreCounter)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
            _healthBar.Construct(health.Max);
            
            _scoreCounter = scoreCounter;
            _scoreCounter.Updated += UpdateScoreBar;
        }


        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;

            if (_scoreCounter != null) 
                _scoreCounter.Updated -= UpdateScoreBar;
        }

        private void UpdateHpBar() => 
            _healthBar.SetValue(_health.Current);
        
        private void UpdateScoreBar(int newScore) => 
            _scoreBar.SetValue(newScore);
    }
}
