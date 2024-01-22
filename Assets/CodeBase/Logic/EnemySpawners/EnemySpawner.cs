using CodeBase.Extensions;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly Vector2 _xSpawnRangeAccordingToScreen;
        private readonly int _ySpawnPointAccordingToScreen;

        public EnemySpawner(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _ySpawnPointAccordingToScreen = Screen.height;
            _xSpawnRangeAccordingToScreen = new Vector2(0, Screen.width);
        }

        public async void SpawnEnemyWaves()
        {
            while (true)
            {
                float randomXPosition = Random.Range(_xSpawnRangeAccordingToScreen.x, _xSpawnRangeAccordingToScreen.y);
                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomXPosition, _ySpawnPointAccordingToScreen, 0));
                
                await UniTask.Delay(3f.ToMilliseconds());
                
                _gameFactory.CreateEnemy(spawnPosition);
            }
        }
    }
}