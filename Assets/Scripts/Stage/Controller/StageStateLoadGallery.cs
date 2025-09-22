using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateLoadGallery : IStageState
    {
        private readonly StageController sC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateLoadGallery(StageController sC)
        {
            this.sC = sC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.FadeOutBGM().Forget();
            Load().Forget();
        }

        private async UniTask Load()
        {
            await sC.CloseStage(false, token);
            SceneManager.LoadScene(sC.GallerySceneName);
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