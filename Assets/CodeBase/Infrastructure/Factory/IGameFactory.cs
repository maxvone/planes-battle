using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    void WarmUp();
    Task<GameObject> CreateHero();
  }
}