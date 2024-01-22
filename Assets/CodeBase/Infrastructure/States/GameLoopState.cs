using CodeBase.Hero;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IPayloadedState<GameObject>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IEnemySpawner _enemySpawner;
    private readonly IWinService _winService;
    private readonly ITimeService _timeService;
    private HeroDeath _heroDeath;

    public GameLoopState(GameStateMachine stateMachine, IEnemySpawner enemySpawner, IWinService winService, ITimeService timeService)
    {
      _stateMachine = stateMachine;
      _enemySpawner = enemySpawner;
      _winService = winService;
      _timeService = timeService;
    }

    public void Enter(GameObject hero)
    {
      _enemySpawner.SpawnEnemyWaves();
      _heroDeath = hero.GetComponent<HeroDeath>();
      _heroDeath.Happened += EnterLostState;
      _winService.Won += EnterWinState;
      
      _timeService.StartTicking();
    }

    private void EnterLostState() => 
      _stateMachine.Enter<LostState>();

    private void EnterWinState() => 
      _stateMachine.Enter<WinState>();

    public void Exit()
    {
      _heroDeath.Happened -= EnterLostState;
      _winService.Won -= EnterWinState;
    }
  }
}