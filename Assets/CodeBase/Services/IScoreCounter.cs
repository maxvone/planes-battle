using System;
using CodeBase.Services;

namespace CodeBase.Infrastructure.States
{
    public interface IScoreCounter : IService
    {
        void Add(int scorePoints);
        event Action<int> Updated;
    }
}