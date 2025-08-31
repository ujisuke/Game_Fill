using System.Threading;
using Assets.Scripts.Map.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateLoadMap : ITitleState
    {
        private readonly TitleController tC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public TitleStateLoadMap(TitleController tC)
        {
            this.tC = tC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            MapStateInitial.OpenFromTitle();
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await tC.CloseScene(token);
            SceneManager.LoadScene(tC.SelectStageSceneName);
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
