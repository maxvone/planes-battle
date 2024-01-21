using CodeBase.Enemies;
using CodeBase.Enemy;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyPoolSpawner
    {
        private IObjectPool<GameObject> _enemiesPool;
        private GameObject _enemyPrefab;

        public EnemyPoolSpawner() => 
            _enemiesPool = new ObjectPool<GameObject>(CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy);

        public void Construct(GameObject enemyPrefab) => 
            _enemyPrefab = enemyPrefab;

        public GameObject Get(Vector2 at)
        {
            GameObject enemy = _enemiesPool.Get();
            enemy.transform.position = at;
            enemy.GetComponent<EnemyHealth>()?.ResetHealth();
            enemy.GetComponent<SpriteRenderer>().enabled = true;
            enemy.GetComponent<OutOfScreenFromBottomNotifier>().Reset();
            enemy.GetComponent<EnemyAttack>().StartAttackProcess();

            return enemy;
        }

        public void Release(GameObject enemy) =>
            _enemiesPool.Release(enemy);

        private GameObject CreateEnemy()
        {
            GameObject enemy = Object.Instantiate(_enemyPrefab);
            enemy.GetComponent<EnemyDeath>()?.SetPool(this);

            return enemy;
        }

        private void OnGetEnemy(GameObject enemy)
        {
            enemy.SetActive(true);
        }

        private void OnReleaseEnemy(GameObject enemy) =>
            enemy.SetActive(false);
        
        private void OnDestroyEnemy(GameObject enemy) => 
            Object.Destroy(enemy);
    }
}