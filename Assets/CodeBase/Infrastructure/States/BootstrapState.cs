using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.Input;

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
    
    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState>();

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IGameStateMachine>(_stateMachine);
      RegisterAssetProvider();
      _services.RegisterSingle<IInputService>(new InputService());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
        _services.Single<IInputService>()));
      _services.RegisterSingle<IEnemySpawner>(new EnemySpawner(_services.Single<IGameFactory>()));
    }
    
    private void RegisterAssetProvider()
    {
      AssetProvider assetProvider = new AssetProvider();
      _services.RegisterSingle<IAssetProvider>(assetProvider);
      assetProvider.Initialize();
    }
  }
}