using System;
using CodeBase.Infrastructure.States;

namespace CodeBase.Services
{
    public class SecondsPastWinCondition : IWinConditionStrategy
    {
        public event Action Achieved;

        private readonly int _secondsToWin;
        private readonly ITimeService _timeService;

        public SecondsPastWinCondition(int secondsToWin)
        {
            _secondsToWin = secondsToWin;

            _timeService = AllServices.Container.Single<ITimeService>();
            _timeService.SecondTick += OnSecondsTick;
        }

        private void OnSecondsTick(int secondsPassed)
        {
            if (secondsPassed < _secondsToWin) return;

            Achieved?.Invoke();
            _timeService.SecondTick -= OnSecondsTick;
        }
    }
}