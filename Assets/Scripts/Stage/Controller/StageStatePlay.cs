using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStatePlay : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStatePlay(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (PlayerModel.Instance != null && StageModel.Instance.IsAllBlockFilled() && PlayerModel.Instance.IsOnExit)
                sSM.ChangeState(new StageStateClear(sSM, sC));
            else if (PlayerModel.Instance == null)
                sSM.ChangeState(new StageStateDead(sSM, sC));
        }

        public void OnStateExit()
        {

        }
    }
}