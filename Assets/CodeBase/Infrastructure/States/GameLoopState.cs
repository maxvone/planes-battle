using CodeBase.Hero;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IPayloadedState<GameObject>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IEnemySpawner _enemySpawner;
    private HeroDeath _heroDeath;

    public GameLoopState(GameStateMachine stateMachine, IEnemySpawner enemySpawner)
    {
      _stateMachine = stateMachine;
      _enemySpawner = enemySpawner;
    }

    public void Enter(GameObject hero)
    {
      _enemySpawner.SpawnEnemyWaves();
      _heroDeath = hero.GetComponent<HeroDeath>();
      _heroDeath.Happened += EnterLostState;
    }

    private void EnterLostState()
    {
      _stateMachine.Enter<LostState>();
    }

    public void Exit()
    {
      _heroDeath.Happened -= EnterLostState;
    }
  }
}