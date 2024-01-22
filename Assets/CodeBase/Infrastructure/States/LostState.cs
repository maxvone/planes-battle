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
        private readonly ITimeService _timeService;

        public LostState(GameStateMachine gameStateMachine, IScoreCounter scoreCounter, IUIFactory uiFactory, ITimeService timeService)
        {
            _gameStateMachine = gameStateMachine;
            _scoreCounter = scoreCounter;
            _uiFactory = uiFactory;
            _timeService = timeService;
        }
        
        public void Enter()
        {
            CreateGameOverWindow();
            _timeService.StopTicking();
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