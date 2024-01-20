using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Task WarmUp();
    Task<GameObject> CreateHero(Vector3 at);
  }
}