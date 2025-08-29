using System;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateDead : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStateDead(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {
            StageModel.Instance.DestroyAllBlock();
            Dead().Forget();
        }
        
        private async UniTask Dead()
        {
            sC.CloseStage(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}