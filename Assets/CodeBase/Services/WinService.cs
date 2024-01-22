using System;

namespace CodeBase.Services
{
    public class WinService : IWinService
    {
        private IWinConditionStrategy _winConditionStrategy = new SecondsPastWinCondition(5);
        public event Action Won;

        public WinService()
        {
            _winConditionStrategy.Achieved += OnWinConditionAchieved;
        }

        private void OnWinConditionAchieved()
        {
            Won?.Invoke();
        }

        public void SetWinConditionStrategy(IWinConditionStrategy winConditionStrategy) => 
            _winConditionStrategy = winConditionStrategy;
    }
}