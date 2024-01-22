using System;

namespace CodeBase.Services
{
    public interface IScoreCounter : IService
    {
        void Add(int scorePoints);
        event Action<int> Updated;
    }
}