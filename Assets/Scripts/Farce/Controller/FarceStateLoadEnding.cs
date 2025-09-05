using System.Threading;
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
