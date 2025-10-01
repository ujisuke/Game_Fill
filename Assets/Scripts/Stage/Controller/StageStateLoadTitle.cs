using System.Threading;
using Assets.Scripts.AudioSource.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateLoadTitle : IStageState
    {
        private readonly StageController sC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateLoadTitle(StageController sC)
        {
            this.sC = sC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            Load().Forget();
        }

        private async UniTask Load()
        {
            AudioSourceView.Instance.FadeOutBGM().Forget();
            await sC.CloseStageWithBlack(token);
            SceneManager.LoadScene(sC.TitleSceneName);
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