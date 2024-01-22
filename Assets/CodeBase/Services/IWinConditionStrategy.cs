using System;

namespace CodeBase.Services
{
    public interface IWinConditionStrategy
    {
        event Action Achieved;
    }
}