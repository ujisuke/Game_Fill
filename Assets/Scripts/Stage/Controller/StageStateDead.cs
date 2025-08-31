using System;
using System.Threading;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateDead : IStageState
    {
        private readonly StageController sC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateDead(StageController sC)
        {
            this.sC = sC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            StageModel.Instance.DestroyAllBlock();
            Dead().Forget();
        }
        
        private async UniTask Dead()
        {
            await sC.CloseStage(true, token);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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