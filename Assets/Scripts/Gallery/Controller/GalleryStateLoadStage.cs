using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Gallery.Controller
{
    public class GalleryStateLoadStage : IGalleryState
    {
        private readonly GalleryController gC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public GalleryStateLoadStage(GalleryController gC)
        {
            this.gC = gC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.FadeOutBGM().Forget();
            StageController.SetIsOnGallery(true);
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await gC.CloseScene(token);
            SceneManager.LoadScene(gC.CurrentStageName);
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
