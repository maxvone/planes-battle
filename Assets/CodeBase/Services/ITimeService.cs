using System;
using CodeBase.Services;

namespace CodeBase.Infrastructure.States
{
    public interface ITimeService : IService
    {
        event Action<int> SecondTick;
        void StartTicking();
    }
}