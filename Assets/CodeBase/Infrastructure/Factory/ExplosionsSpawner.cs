using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{
    public class ExplosionsSpawner
    {
        private IObjectPool<GameObject> _explosionsPool;
        private GameObject _explosionPrefab;

        public ExplosionsSpawner() => 
            _explosionsPool = new ObjectPool<GameObject>(CreateExplosion, OnGetExplosion, OnReleaseExplosion, OnDestroyExplosion);

        public void Construct(GameObject bulletPrefab) => 
            _explosionPrefab = bulletPrefab;

        public GameObject Get(Vector2 at)
        {
            GameObject explosion = _explosionsPool.Get();
            explosion.transform.position = at;

            return explosion;
        }

        public void Release(GameObject explosion) =>
            _explosionsPool.Release(explosion);

        private GameObject CreateExplosion()
        {
            GameObject explosion = Object.Instantiate(_explosionPrefab);

            return explosion;
        }

        private void OnGetExplosion(GameObject explosion) => 
            explosion.SetActive(true);

        private void OnReleaseExplosion(GameObject explosion) =>
            explosion.SetActive(false);
        
        private void OnDestroyExplosion(GameObject bullet) => 
            Object.Destroy(bullet);

    }
}