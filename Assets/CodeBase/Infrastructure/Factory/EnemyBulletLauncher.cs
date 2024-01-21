using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyBulletLauncher
    {
        private IObjectPool<EnemyBullet> _bulletsPool;
        private GameObject _bulletPrefab;

        public EnemyBulletLauncher() => 
            _bulletsPool = new ObjectPool<EnemyBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet);

        public void Construct(GameObject bulletPrefab) => 
            _bulletPrefab = bulletPrefab;

        public EnemyBullet Get(Vector2 at)
        {
            EnemyBullet heroBullet = _bulletsPool.Get();
            heroBullet.transform.position = at;

            return heroBullet;
        }

        public void Release(EnemyBullet bullet) =>
            _bulletsPool.Release(bullet);

        private EnemyBullet CreateBullet()
        {
            EnemyBullet bullet = Object.Instantiate(_bulletPrefab).GetComponent<EnemyBullet>();
            bullet.SetPool(this);


            return bullet;
        }

        private void OnGetBullet(EnemyBullet bullet) => 
            bullet.gameObject.SetActive(true);

        private void OnReleaseBullet(EnemyBullet bullet) =>
            bullet.gameObject.SetActive(false);
        
        private void OnDestroyBullet(EnemyBullet bullet) => 
            Object.Destroy(bullet.gameObject);
    }
}