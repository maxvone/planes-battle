using System;

namespace CodeBase.Services
{
    public class ScoreCounter : IScoreCounter
    {
        public event Action<int> Updated;
        
        private int _currentScore;
        
        public void Add(int scorePoints)
        {
            _currentScore += scorePoints;
            Updated?.Invoke(_currentScore);
        }
    }
}