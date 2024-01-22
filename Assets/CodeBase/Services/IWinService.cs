using System;

namespace CodeBase.Services
{
    public interface IWinService : IService
    {
        event Action Won;
        void SetWinConditionStrategy(IWinConditionStrategy winConditionStrategy);
    }
}