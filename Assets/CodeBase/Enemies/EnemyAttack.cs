using System.Threading.Tasks;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _fireRateInSeconds = 2f;
        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _heroTransform = gameFactory.HeroInstance.transform;
        }

        private async void StartAttackProcess()
        {
            await WaitForReload();

            while (HeroWithinReach())
            {
                Attack();
                await WaitForReload();
            }
        }

        private async Task WaitForReload() => 
            await UniTask.Delay(_fireRateInSeconds.ToMilliseconds());

        private bool HeroWithinReach() => 
            _heroTransform.position.y <= transform.position.y;

        private void Attack() => 
            _gameFactory.CreateEnemyBullet(transform.position);
    }
}
