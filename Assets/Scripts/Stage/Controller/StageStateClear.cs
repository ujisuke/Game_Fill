using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateClear : IStageState
    {
        private readonly StageController sC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateClear(StageController sC)
        {
            this.sC = sC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            Clear().Forget();
        }

        private async UniTask Clear()
        {
            if (sC.IsFinalStage)
            {
                await sC.PlayClearFinalEffect(token);
                SceneManager.LoadScene(sC.FarceSceneName);
            }
            else
            {
                await sC.PlayClearEffect(token);
                SceneManager.LoadScene(sC.NextStageName);
            }
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