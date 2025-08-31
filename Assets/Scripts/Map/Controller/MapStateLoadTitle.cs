using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateLoadTitle : IMapState
    {
        private readonly MapController mC;

        public MapStateLoadTitle(MapController mC)
        {
            this.mC = mC;
        }

        public void OnStateEnter()
        {
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            await mC.CloseSceneToTitle();
            SceneManager.LoadScene(mC.TitleSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
