namespace Assets.Scripts.Stage.Controller
{
    public class StageStateMachine
    {
        private IStageState currentState;

        public StageStateMachine(StageController stageController)
        {
            currentState = new StageStateInitial(this, stageController);
            currentState.OnStateEnter();
        }

        public void ChangeState(IStageState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void Update()
        {
            currentState.HandleInput();
        }
    }
}
