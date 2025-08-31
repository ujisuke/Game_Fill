using Assets.Scripts.Map.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateLoadMap : ITitleState
    {
        private readonly TitleController tC;

        public TitleStateLoadMap(TitleController tC)
        {
            this.tC = tC;
        }

        public void OnStateEnter()
        {
            MapStateInitial.OpenFromTitle();
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await tC.CloseScene();
            SceneManager.LoadScene(tC.SelectStageSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
