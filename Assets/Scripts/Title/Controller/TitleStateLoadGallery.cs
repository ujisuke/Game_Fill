using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Gallery.Controller;
using Assets.Scripts.Map.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateLoadGallery : ITitleState
    {
        private readonly TitleController tC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public TitleStateLoadGallery(TitleController tC)
        {
            this.tC = tC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            GalleryStateInitial.OpenFromTitle();
            AudioSourceView.Instance.FadeOutBGM().Forget();
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await tC.CloseSceneWithBlack(token);
            SceneManager.LoadScene(tC.GallerySceneName);
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
