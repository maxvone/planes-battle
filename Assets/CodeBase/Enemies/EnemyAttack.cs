using System;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(EnemyDeath))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private float _fireRateInSeconds = 2f;
        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private bool _started;

        private void OnEnable() => 
            _enemyDeath.Happened += StopAttackProcess;
        
        private void OnDisable() => 
            _enemyDeath.Happened -= StopAttackProcess;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            
            if (gameFactory.HeroInstance != null) 
                _heroTransform = gameFactory.HeroInstance.transform;
        }

        public async void StartAttackProcess()
        {
            _started = true;
            await WaitForReload();

            while (HeroWithinReach() && _started)
            {
                Attack();
                await WaitForReload();
            }
        }

        public void StopAttackProcess() => 
            _started = false;

        private async Task WaitForReload() => 
            await UniTask.Delay(_fireRateInSeconds.ToMilliseconds());

        private bool HeroWithinReach() => 
            _heroTransform != null && _heroTransform.position.y <= transform.position.y;

        private void Attack() => 
            _gameFactory.CreateEnemyBullet(transform.position);
    }
}
