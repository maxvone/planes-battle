using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITimeService _timeService;
        private readonly IUIFactory _uiFactory;

        public WinState(GameStateMachine gameStateMachine, ITimeService timeService, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _timeService = timeService;
            _uiFactory = uiFactory;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _timeService.StopTicking();
            CreateWinWindow();
        }
        private async void CreateWinWindow()
        {
            await _uiFactory.CreateWinWindow();
        }
    }
}