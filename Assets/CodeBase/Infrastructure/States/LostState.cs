using CodeBase.Extensions;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class LostState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IScoreCounter _scoreCounter;
        private readonly IUIFactory _uiFactory;

        public LostState(GameStateMachine gameStateMachine, IScoreCounter scoreCounter, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _scoreCounter = scoreCounter;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            CreateGameOverWindow();
        }

        private async void CreateGameOverWindow()
        {
            await UniTask.Delay(2f.ToMilliseconds());
            await _uiFactory.CreateGameOverWindow();
        }

        public void Exit()
        {
        }

    }
}