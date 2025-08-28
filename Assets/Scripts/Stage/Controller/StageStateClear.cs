using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateClear : IStageState
    {
        private readonly StageStateMachine sSM;

        public StageStateClear(StageStateMachine sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            Clear().Forget();
        }

        private async UniTask Clear()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            if (sSM.IsFinalStage)
                SceneManager.LoadScene(sSM.NextStageName);
            else
                SceneManager.LoadScene(sSM.MapSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}