using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Task WarmUp();
    Task<GameObject> CreateHero(Vector2 at);
    Task<GameObject> InstantiateAsync(string prefabPath, Vector2 at);
    HeroBullet CreateHeroBullet(Vector2 at);
    GameObject CreateExplosion(Vector2 at);
    Task<GameObject> CreateEnemy(Vector2 at);
  }
}