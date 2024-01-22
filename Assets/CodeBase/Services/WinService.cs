using System;
using UnityEngine;

namespace CodeBase.Services
{
    public class WinService : IWinService
    {
        private IWinConditionStrategy _winConditionStrategy = new ScoreAchievedWinCondition(200);
        public event Action Won;

        public WinService()
        {
            _winConditionStrategy.Achieved += OnWinConditionAchieved;
        }

        private void OnWinConditionAchieved()
        {
            Won?.Invoke();
            Debug.Log("WON");
        }

        public void SetWinConditionStrategy(IWinConditionStrategy winConditionStrategy) => 
            _winConditionStrategy = winConditionStrategy;
    }
}