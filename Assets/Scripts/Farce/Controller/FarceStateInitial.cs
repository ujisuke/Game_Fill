using Assets.Scripts.Common.Controller;
using Assets.Scripts.Farce.Model;

namespace Assets.Scripts.Farce.Controller
{
    public class FarceStateInitial : IFarceState
    {
        private readonly FarceController fC;
        private readonly FarceStateMachine fSM;

        public FarceStateInitial(FarceStateMachine fSM, FarceController fC)
        {
            this.fC = fC;
            this.fSM = fSM;
        }

        public void OnStateEnter()
        {
            fC.OpenScene();
            FarceModel.SaveClearStage();
            Assets.Steam.Scripts.Achievements.CheckAchievementOnFarce();
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetPauseKeyWithCooldown())
                fSM.ChangeState(new FarceStatePause(fSM, fC));
            else if (CustomInputSystem.Instance.GetSelectKeyUp())
            {
                if (fC.IsEnding)
                    fSM.ChangeState(new FarceStateLoadEnding(fC));
                else
                    fSM.ChangeState(new FarceStateLoadMap(fC));
            }
        }

        public void OnStateExit()
        {

        }
    }
}
