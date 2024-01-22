using System.Threading.Tasks;
using CodeBase.Enemies;
using CodeBase.Hero;
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
    GameObject CreateEnemy(Vector2 at);
    EnemyBullet CreateEnemyBullet(Vector3 transformPosition);
    GameObject HeroInstance { get; }
    Task<GameObject> CreateHud();
  }
}