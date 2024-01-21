using CodeBase.Services;

namespace CodeBase.Logic.EnemySpawners
{
    public interface IEnemySpawner : IService
    {
        void SpawnEnemyWaves();
    }
}