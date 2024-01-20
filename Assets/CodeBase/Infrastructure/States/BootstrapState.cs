using CodeBase.Services;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, AllServices services)
    {
      _stateMachine = stateMachine;
      _services = services;

      RegisterServices();
    }

    public void Enter()
    {
      EnterLoadLevel();  
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
    }
    
    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState>();
  }
}