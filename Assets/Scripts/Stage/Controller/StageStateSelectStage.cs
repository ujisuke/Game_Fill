using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateSelectStage : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStateSelectStage(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {
            Load().Forget();
        }

        private async UniTask Load()
        {
            await sC.CloseStage(false);
            SceneManager.LoadScene(sC.MapSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}