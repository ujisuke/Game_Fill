using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStatePlay : IStageState
    {
        private readonly StageStateMachine sSM;

        public StageStatePlay(StageStateMachine sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (PlayerModel.Instance != null && StageModel.Instance.IsAllBlockFilled() && PlayerModel.Instance.IsOnExit)
                sSM.ChangeState(new StageStateClear(sSM));
            else if (PlayerModel.Instance == null)
                sSM.ChangeState(new StageStateDead(sSM));
        }

        public void OnStateExit()
        {

        }
    }
}