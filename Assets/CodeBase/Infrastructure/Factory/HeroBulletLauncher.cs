using CodeBase.Hero;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroBulletLauncher
    {
        private IObjectPool<HeroBullet> _bulletsPool;
        private GameObject _bulletPrefab;

        public HeroBulletLauncher() => 
            _bulletsPool = new ObjectPool<HeroBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet);

        public void Construct(GameObject bulletPrefab) => 
            _bulletPrefab = bulletPrefab;

        public HeroBullet Get(Vector2 at)
        {
            HeroBullet heroBullet = _bulletsPool.Get();
            heroBullet.transform.position = at;

            return heroBullet;
        }

        public void Release(HeroBullet bullet) =>
            _bulletsPool.Release(bullet);

        private HeroBullet CreateBullet()
        {
            HeroBullet heroBullet = Object.Instantiate(_bulletPrefab).GetComponent<HeroBullet>();
            heroBullet.Construct(_bulletsPool);


            return heroBullet;
        }

        private void OnGetBullet(HeroBullet bullet) => 
            bullet.gameObject.SetActive(true);

        private void OnReleaseBullet(HeroBullet bullet) =>
            bullet.gameObject.SetActive(false);
        
        private void OnDestroyBullet(HeroBullet bullet) => 
            Object.Destroy(bullet.gameObject);

    }
}