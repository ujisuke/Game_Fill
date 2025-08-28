namespace Assets.Scripts.Stage.Controller
{
    public class StageStateInitial : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStateInitial(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {
            sC.OpenStage();
        }

        public void HandleInput()
        {
            sSM.ChangeState(new StageStatePlay(sSM, sC));
        }

        public void OnStateExit()
        {

        }
    }
}