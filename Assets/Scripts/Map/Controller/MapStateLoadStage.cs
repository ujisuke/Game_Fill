using System.Threading;
using Assets.Scripts.AudioSource.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateLoadStage : IMapState
    {
        private readonly MapController mC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public MapStateLoadStage(MapController mC)
        {
            this.mC = mC;
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
            await mC.CloseScene(token);
            SceneManager.LoadScene(mC.CurrentStageName);
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
