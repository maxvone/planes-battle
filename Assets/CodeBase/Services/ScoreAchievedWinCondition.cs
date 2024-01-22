using System;
using CodeBase.Infrastructure.States;

namespace CodeBase.Services
{
    public class ScoreAchievedWinCondition : IWinConditionStrategy
    {
        public event Action Achieved;

        private readonly int _scoreToWin;
        private readonly IScoreCounter _scoreCounter;

        public ScoreAchievedWinCondition(int scoreToWin)
        {
            _scoreToWin = scoreToWin;

            _scoreCounter = AllServices.Container.Single<IScoreCounter>();
            _scoreCounter.Updated += OnScoreUpdated;
        }

        private void OnScoreUpdated(int score)
        {
            if (score < _scoreToWin) return;
            
            Achieved?.Invoke();
            _scoreCounter.Updated -= OnScoreUpdated;
        }
    }
}