using System;
using CodeBase.Extensions;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services
{
    public class TimeService : ITimeService
    {
        public event Action<int> SecondTick;
        public int SecondsPassed { get; private set; }

        private bool _ticking;

        public async void StartTicking()
        {
            _ticking = true;
            while (_ticking)
            {
                await UniTask.Delay(1f.ToMilliseconds());
                SecondsPassed++;
                SecondTick?.Invoke(SecondsPassed);
            }
        }

        public void StopTicking() => 
            _ticking = false;
    }
}