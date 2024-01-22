using System;

namespace CodeBase.Infrastructure.States
{
    public class TimeService : ITimeService
    {
        public event Action<int> SecondTick;

        public void StartTicking()
        {
            
        }
    }
}