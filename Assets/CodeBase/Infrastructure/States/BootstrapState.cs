using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Services.Factory;

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
      _services.RegisterSingle<IScoreCounter>(new ScoreCounter());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
        _services.Single<IInputService>(), _services.Single<IScoreCounter>()));
      _services.RegisterSingle<IEnemySpawner>(new EnemySpawner(_services.Single<IGameFactory>()));
      _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>()));
    }
    
    private void RegisterAssetProvider()
    {
      AssetProvider assetProvider = new AssetProvider();
      _services.RegisterSingle<IAssetProvider>(assetProvider);
      assetProvider.Initialize();
    }
  }
}