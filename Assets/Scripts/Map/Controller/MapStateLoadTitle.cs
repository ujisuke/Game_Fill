using System;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateLoadTitle : IMapState
    {
        private readonly MapController mC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public MapStateLoadTitle(MapController mC)
        {
            this.mC = mC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.PlayChooseSE();
            AudioSourceView.Instance.FadeOutBGM().Forget();
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await mC.CloseSceneToTitle(token);
            SceneManager.LoadScene(mC.TitleSceneName);
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
