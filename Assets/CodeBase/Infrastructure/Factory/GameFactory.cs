using System.Threading.Tasks;
using CodeBase.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    private GameObject _heroInstance;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public async Task WarmUp()
    {
      await _assets.Load<GameObject>(AssetAddress.HeroPath);
    }

    public async Task<GameObject> CreateHero(Vector3 at) => 
      _heroInstance = await InstantiateAsync(AssetAddress.HeroPath, at);

    private async Task<GameObject> InstantiateAsync(string prefabPath, Vector3 at)
    {
      GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);

      return gameObject;
    }
  }
}