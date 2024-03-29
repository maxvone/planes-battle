﻿using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, services),
        [typeof(LoadLevelState)] = new LoadLevelState(this,services.Single<IGameFactory>(), 
          services.Single<IUIFactory>(), services.Single<IScoreCounter>()),
        [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IEnemySpawner>(),
          services.Single<IWinService>() ,services.Single<ITimeService>()),
        [typeof(LostState)] = new LostState(this, services.Single<IScoreCounter>(), 
          services.Single<IUIFactory>(), services.Single<ITimeService>()),
        [typeof(WinState)] = new WinState(this, services.Single<ITimeService>(), services.Single<IUIFactory>() )
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}