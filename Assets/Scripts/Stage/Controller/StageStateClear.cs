using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateClear : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStateClear(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {
            Clear().Forget();
        }

        private async UniTask Clear()
        {
            await sC.PlayClearEffect();
            if (sC.IsFinalStage)
                SceneManager.LoadScene(sC.MapSceneName);
            else
                SceneManager.LoadScene(sC.NextStageName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}