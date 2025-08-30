using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateLoadScene : IMapState
    {
        private readonly MapStateMachine mSM;
        private readonly MapController mC;

        public MapStateLoadScene(MapStateMachine stateMachine, MapController mC)
        {
            mSM = stateMachine;
            this.mC = mC;
        }

        public void OnStateEnter()
        {
            LoadScene().Forget();
        }

        private async UniTask LoadScene()
        {
            mC.CloseStage();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            SceneManager.LoadScene(mC.CurrentStageName);
        }
        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
