using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateEnding : IStageState
    {
        private readonly StageController sC;
        private readonly StageStateMachine sSM;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;
        private bool isAbleToLoadTitle;

        public StageStateEnding(StageController sC, StageStateMachine sSM)
        {
            this.sC = sC;
            this.sSM = sSM;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            isAbleToLoadTitle = false;
            End().Forget();
        }

        private async UniTask End()
        {
            await sC.PlayEndingEffect(token);
            isAbleToLoadTitle = true;
            AudioSourceView.Instance.PlayEndingBGM();
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetSelectKeyUp() && isAbleToLoadTitle)
                sSM.ChangeState(new StageStateLoadTitle(sC));
        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}