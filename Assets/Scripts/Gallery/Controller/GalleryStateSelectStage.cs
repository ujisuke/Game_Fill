using System.Threading;
using Assets.Scripts.Common.Controller;

namespace Assets.Scripts.Gallery.Controller
{
    public class GalleryStateSelectStage : IGalleryState
    {
        private readonly GalleryStateMachine gSM;
        private readonly GalleryController gC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public GalleryStateSelectStage(GalleryStateMachine stateMachine, GalleryController gC)
        {
            gSM = stateMachine;
            this.gC = gC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (gC.CurrentStageName[0] != '-')
            {
                if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
                    gC.SelectRight(token);
                else if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
                    gC.SelectLeft(token);
                else if (CustomInputSystem.Instance.GetUpKeyWithCooldown())
                    gC.SetDifficulty(true);
                else if (CustomInputSystem.Instance.GetDownKeyWithCooldown())
                    gC.SetDifficulty(false);
            }

            if (CustomInputSystem.Instance.GetSelectKeyUp() && gC.CurrentStageName != "0-5" && gC.CurrentStageName[0] != '-')
                    gSM.ChangeState(new GalleryStateLoadStage(gC));
            else if (CustomInputSystem.Instance.GetPauseKeyWithCooldown())
                gSM.ChangeState(new GalleryStateLoadTitle(gC));
        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}
