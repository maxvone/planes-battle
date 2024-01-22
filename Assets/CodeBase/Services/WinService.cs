using System;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services
{
    public class WinService : IWinService
    {
        private const string StaticDataWinConditionsPath = "StaticData/WinConditions";
        private IWinConditionStrategy _winConditionStrategy = new SecondsPastWinCondition(5);
        private WinConditions _winConditions;
        public event Action Won;

        public WinService()
        {
            _winConditions = Resources.Load(StaticDataWinConditionsPath) as WinConditions;
            ChooseStrategy();
            _winConditionStrategy.Achieved += OnWinConditionAchieved;
        }
        
        private void ChooseStrategy()
        {
            switch (_winConditions.WinCondition)
            {
                case WinCondition.SecondsToWin:
                    SetWinConditionStrategy(new SecondsPastWinCondition(_winConditions.SecondToWin));
                    break;
                case WinCondition.ScoreToAchieve:
                    SetWinConditionStrategy(new ScoreAchievedWinCondition(_winConditions.ScoreToAchieve));
                    break;
            }
        }

        private void OnWinConditionAchieved() => 
            Won?.Invoke();

        public void SetWinConditionStrategy(IWinConditionStrategy winConditionStrategy) => 
            _winConditionStrategy = winConditionStrategy;
    }
}