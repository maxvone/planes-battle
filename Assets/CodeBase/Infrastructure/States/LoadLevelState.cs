using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IUIFactory uiFactory)
    {
      _stateMachine = gameStateMachine;
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
    }

    public async void Enter()
    {
      await _gameFactory.WarmUp();
      await InitUIRoot();
      await InitLevel(); 
    }

    private async Task InitUIRoot() => 
      await _uiFactory.CreateUIRoot();

    private async Task InitLevel()
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
    
    public void Exit()
    {
      
    }
    
  }
}