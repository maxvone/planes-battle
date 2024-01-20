using System.Threading.Tasks;
using CodeBase.AssetManagement;
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

        public GameFactory(IAssetProvider assets, IInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
            _heroBulletLauncher = new HeroBulletLauncher();
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.HeroPath);
            GameObject bulletPrefab = await _assets.Load<GameObject>(AssetAddress.HeroBulletPath);
            _heroBulletLauncher.Construct(bulletPrefab);
        }

        public async Task<GameObject> CreateHero(Vector2 at)
        {
            _heroInstance = await InstantiateAsync(AssetAddress.HeroPath, at);
            _heroInstance.GetComponent<HeroMove>().Construct(_inputService);
            _heroInstance.GetComponent<HeroAttack>().Construct(_inputService, this);

            return _heroInstance;
        }

        public HeroBullet CreateHeroBullet(Vector2 at) => 
            _heroBulletLauncher.Get(at);

        public async Task<GameObject> InstantiateAsync(string prefabPath, Vector2 at)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);

            return gameObject;
        }
    }
}