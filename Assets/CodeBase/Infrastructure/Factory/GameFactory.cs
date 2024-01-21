using System.Threading.Tasks;
using CodeBase.AssetManagement;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private GameObject _heroInstance;
        private HeroBulletLauncher _heroBulletLauncher;
        private ExplosionsSpawner _explosionsSpawner;

        public GameFactory(IAssetProvider assets, IInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
            _heroBulletLauncher = new HeroBulletLauncher();
            _explosionsSpawner = new ExplosionsSpawner();
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.HeroPath);
            await _assets.Load<GameObject>(AssetAddress.EnemyPath);
            
            GameObject bulletPrefab = await _assets.Load<GameObject>(AssetAddress.HeroBulletPath);
            _heroBulletLauncher.Construct(bulletPrefab);

            GameObject explosionPrefab = await _assets.Load<GameObject>(AssetAddress.ExplosionPath);
            _explosionsSpawner.Construct(explosionPrefab);

        }

        public async Task<GameObject> CreateHero(Vector2 at)
        {
            _heroInstance = await InstantiateAsync(AssetAddress.HeroPath, at);
            _heroInstance.GetComponent<HeroMove>().Construct(_inputService);
            _heroInstance.GetComponent<HeroAttack>().Construct(_inputService, this);

            return _heroInstance;
        }
        
        public async Task<GameObject> CreateEnemy(Vector2 at)
        {
            _heroInstance = await InstantiateAsync(AssetAddress.EnemyPath, at);
            _heroInstance.GetComponent<EnemyDeath>().Construct(this);

            return _heroInstance;
        }

        public HeroBullet CreateHeroBullet(Vector2 at) => 
            _heroBulletLauncher.Get(at);

        public GameObject CreateExplosion(Vector2 at) => 
            _explosionsSpawner.Get(at);
        
        public async Task<GameObject> InstantiateAsync(string prefabPath, Vector2 at)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);

            return gameObject;
        }
    }
}