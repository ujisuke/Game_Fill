using System.Threading;
using Assets.Scripts.Map.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateLoadTutorial : ITitleState
    {
        private readonly TitleController tC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public TitleStateLoadTutorial(TitleController tC)
        {
            this.tC = tC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await tC.CloseSceneToTutorial(token);
            SceneManager.LoadScene(tC.TutorialSceneName);
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
