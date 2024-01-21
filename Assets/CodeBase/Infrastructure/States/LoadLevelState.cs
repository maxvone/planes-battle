using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
    {
      _stateMachine = gameStateMachine;
      _gameFactory = gameFactory;
    }

    public void Enter()
    {
      _gameFactory.WarmUp();
      InitLevel(); 
    }

    public void Exit()
    {
      
    }

    private async void InitLevel()
    {
      await InitGameWorld();

      _stateMachine.Enter<GameLoopState>();
    }

    private async Task InitGameWorld()
    {
      GameObject hero = await InitHero();
    }

    private async Task<GameObject> InitHero() => 
      await _gameFactory.CreateHero(Vector3.zero);
  }
}