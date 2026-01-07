using System.Threading;
using Assets.Scripts.AudioSource.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateClear : IStageState
    {
        private readonly StageController sC;
        private readonly StageStateMachine sSM;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateClear(StageController sC, StageStateMachine sSM)
        {
            this.sC = sC;
            this.sSM = sSM;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            Clear().Forget();
        }

        private async UniTask Clear()
        {
            if (StageController.IsInGallery)
            {
                AudioSourceView.Instance.FadeOutBGM().Forget();
                await sC.PlayClearEffect(token);
                SceneManager.LoadScene(sC.GallerySceneName);
            }
            else if (sC.IsFinalStage)
            {
                AudioSourceView.Instance.FadeOutBGM().Forget();
                await sC.PlayClearFinalEffect(token);
                SceneManager.LoadScene(sC.NextSceneName);
            }
            else if (sC.IsEndingStage)
                sSM.ChangeState(new StageStateEnding(sC, sSM));
            else
            {
                await sC.PlayClearEffect(token);
                SceneManager.LoadScene(sC.NextSceneName);
            }
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}