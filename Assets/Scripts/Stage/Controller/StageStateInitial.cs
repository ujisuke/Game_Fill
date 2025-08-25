namespace Assets.Scripts.Stage.Controller
{
    public class StageStateInitial : IStageState
    {
        private readonly StageStateMachine sSM;

        public StageStateInitial(StageStateMachine sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            sSM.ChangeState(new StageStatePlay(sSM));
        }

        public void OnStateExit()
        {

        }
    }
}