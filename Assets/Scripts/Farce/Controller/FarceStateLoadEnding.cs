using System.Threading;
using Assets.Scripts.AudioSource.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Farce.Controller
{
    public class FarceStateLoadEnding : IFarceState
    {
        private readonly FarceController fC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public FarceStateLoadEnding(FarceController fC)
        {
            this.fC = fC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.FadeOutBGM().Forget();
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await fC.CloseScene(token);
            SceneManager.LoadScene(fC.EndingSceneName);
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
