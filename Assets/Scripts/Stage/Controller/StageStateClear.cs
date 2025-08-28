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