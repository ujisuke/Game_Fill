namespace Assets.Scripts.Farce.Controller
{
    public class FarceStateMachine
    {
        private IFarceState currentState;

        public FarceStateMachine(FarceController fC)
        {
            currentState = new FarceStateInitial(this, fC);
            currentState.OnStateEnter();
        }

        public void ChangeState(IFarceState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }
    }
}
