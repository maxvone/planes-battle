namespace CodeBase.Infrastructure.States
{
    public class WinState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public WinState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}