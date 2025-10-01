using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Gallery.Model;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Gallery.Controller
{
    public class GalleryStateLoadTitle : IGalleryState
    {
        private readonly GalleryController gC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public GalleryStateLoadTitle(GalleryController gC)
        {
            this.gC = gC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            GalleryModel.ReSetDifficulty();
            AudioSourceView.Instance.PlayChooseSE();
            AudioSourceView.Instance.FadeOutBGM().Forget();
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await gC.CloseSceneToTitle(token);
            SceneManager.LoadScene(gC.TitleSceneName);
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
