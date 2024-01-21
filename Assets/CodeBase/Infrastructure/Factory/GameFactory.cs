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

        public GameObject HeroInstance { get; private set; }
        private HeroBulletLauncher _heroBulletLauncher;
        private ExplosionsSpawner _explosionsSpawner;
        private EnemyPoolSpawner _enemySpawner;

        public GameFactory(IAssetProvider assets, IInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
            _heroBulletLauncher = new HeroBulletLauncher();
            _explosionsSpawner = new ExplosionsSpawner();
            _enemySpawner = new EnemyPoolSpawner();
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.HeroPath);
            
            GameObject enemyPrefab = await _assets.Load<GameObject>(AssetAddress.EnemyPath);
            _enemySpawner.Construct(enemyPrefab);
            
            GameObject bulletPrefab = await _assets.Load<GameObject>(AssetAddress.HeroBulletPath);
            _heroBulletLauncher.Construct(bulletPrefab);

            GameObject explosionPrefab = await _assets.Load<GameObject>(AssetAddress.ExplosionPath);
            _explosionsSpawner.Construct(explosionPrefab);

        }

        public async Task<GameObject> CreateHero(Vector2 at)
        {
            HeroInstance = await InstantiateAsync(AssetAddress.HeroPath, at);
            HeroInstance.GetComponent<HeroMove>().Construct(_inputService);
            HeroInstance.GetComponent<HeroAttack>().Construct(_inputService, this);

            return HeroInstance;
        }
        
        public GameObject CreateEnemy(Vector2 at)
        {
            GameObject enemy = _enemySpawner.Get(at);
            enemy.GetComponent<EnemyDeath>().Construct(this, _explosionsSpawner);

            return enemy;
        }

        public HeroBullet CreateHeroBullet(Vector2 at) => 
            _heroBulletLauncher.Get(at);

        public GameObject CreateExplosion(Vector2 at) => 
            _explosionsSpawner.Get(at);
        
        public EnemyBullet CreateEnemyBullet(Vector3 transformPosition)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<GameObject> InstantiateAsync(string prefabPath, Vector2 at)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);

            return gameObject;
        }
    }
}