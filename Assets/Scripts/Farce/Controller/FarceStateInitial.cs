using Assets.Scripts.Common.Controller;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;

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
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetPauseKeyWithCooldown())
                fSM.ChangeState(new FarceStatePause(fSM, fC));
            else if (CustomInputSystem.Instance.DoesSelectKeyUp())
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
