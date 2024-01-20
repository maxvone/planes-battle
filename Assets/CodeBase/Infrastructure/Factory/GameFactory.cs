using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public void WarmUp()
    {
      throw new System.NotImplementedException();
    }

    public Task<GameObject> CreateHero()
    {
      throw new System.NotImplementedException();
    }
  }
}