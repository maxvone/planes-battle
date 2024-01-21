using CodeBase.Logic.EnemySpawners;

namespace CodeBase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly IEnemySpawner _enemySpawner;

    public GameLoopState(GameStateMachine stateMachine, IEnemySpawner enemySpawner)
    {
      _enemySpawner = enemySpawner;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
      _enemySpawner.SpawnEnemyWaves();
    }
  }
}