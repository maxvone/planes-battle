using CodeBase.Extensions;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Transform _heroTransform;
        private Vector3 _directionToMove;
        private EnemyBulletLauncher _enemyBulletPool;
        private IGameFactory _gameFactory;
        private ExplosionsSpawner _explosionsSpawner;

        public void Construct(Transform heroTransform, EnemyBulletLauncher enemyBulletPool,
            IGameFactory gameFactory, ExplosionsSpawner explosionsSpawner)
        {
            _heroTransform = heroTransform;
            _enemyBulletPool = enemyBulletPool;
            _gameFactory = gameFactory;
            _explosionsSpawner = explosionsSpawner;
            SetupDirection();
        }

        private void SetupDirection()
        {
            _directionToMove = (_heroTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(_directionToMove.y, _directionToMove.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void SetPool(EnemyBulletLauncher enemyBulletPool) => 
            _enemyBulletPool = enemyBulletPool;

        private void Update() => 
            Move();

        private void Move() => 
            transform.position += _directionToMove * _speed * Time.deltaTime;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var health = col.GetComponent<IHealth>();
            health?.TakeDamage(1);
            CreateExplosion();
            _enemyBulletPool.Release(this);
            
        }

        private async void CreateExplosion()
        {
            GameObject explosion = _gameFactory.CreateExplosion(transform.position);
            await UniTask.Delay(1f.ToMilliseconds());
            _explosionsSpawner.Release(explosion);
        }
    }
}