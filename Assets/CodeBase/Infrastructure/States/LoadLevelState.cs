using System.Threading.Tasks;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.UI.Elements;
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
    private readonly IScoreCounter _scoreCounter;
    private GameObject _hero;

    public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IUIFactory uiFactory, IScoreCounter scoreCounter)
    {
      _stateMachine = gameStateMachine;
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
      _scoreCounter = scoreCounter;
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
       await InitHud();

      _stateMachine.Enter<GameLoopState, GameObject>(_hero);
    }

    private async Task InitGameWorld()
    {
      _hero = await InitHero();
    }

    private async Task<GameObject> InitHero() => 
      await _gameFactory.CreateHero(Vector3.zero);
    
    private async Task InitHud()
    {
      GameObject hud = await _gameFactory.CreateHud();
      hud.transform.SetParent(_uiFactory.UiRoot);
      hud.GetComponentInChildren<ActorUI>().Construct(_hero.GetComponent<HeroHealth>(), _scoreCounter);
    }
    
    public void Exit()
    {
      
    }
    
  }
}